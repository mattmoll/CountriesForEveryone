using AutoMapper;
using CountriesForEveryone.Adapter.Models;
using CountriesForEveryone.Core.Exceptions;
using CountriesForEveryone.Core.Validator;
using System.Net.Http.Json;

namespace CountriesForEveryone.Adapter.Adapters
{
    public class BaseAdapter
    {
        public BaseAdapter(HttpClient httpClient, IMapper mapper)
        {
            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }


        protected HttpClient HttpClient { get; }
        protected IMapper Mapper { get; }

        protected async Task<IValidatedEntity<TResult>> GetValidatedEntity<TResult, TDto>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDto = await httpResponseMessage.Content.ReadFromJsonAsync<TDto>();
                return new ValidatedEntity<TResult>
                {
                    Data = Mapper.Map<TResult>(responseDto)
                };
            }

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var responseDto = await httpResponseMessage.Content.ReadFromJsonAsync<ErrorDto>();
                return new ValidatedEntity<TResult>
                {
                    Errors = responseDto.Errors.Select(x => new ValidationError { Message = x }).ToList()
                };
            }

            throw new ExternalApiException("Unexpected error from API");
        }

        protected async Task<IValidatedEntity<TResult>> GetValidatedEntityFromResponseDto<TResult, TDto>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDto = await httpResponseMessage.Content.ReadFromJsonAsync<ResponseDto<TDto>>();

                if (!string.IsNullOrEmpty(responseDto.Error))
                    throw new ExternalApiException(responseDto.Error);

                var validatedEntity = new ValidatedEntity<TResult>();

                if (responseDto.ValidationErrors != null && responseDto.ValidationErrors.Any())
                {
                    validatedEntity.Errors.AddRange(responseDto.ValidationErrors.Select(validationError => new ValidationError
                    {
                        Message = validationError
                    }));
                }
                else
                {
                    validatedEntity.Data = Mapper.Map<TResult>(responseDto.Data);
                }

                return validatedEntity;
            }

            throw new ExternalApiException("Unexpected error from API");
        }

        protected static IValidatedEntity GetValidatedEntityFromResponseDto(ResponseDto responseDto)
        {
            if (responseDto == null)
                throw new ExternalApiException("Unable to get a response from endpoint");

            if (!string.IsNullOrEmpty(responseDto.Error))
                throw new ExternalApiException(responseDto.Error);

            var validatedEntity = new ValidatedEntity();

            if (responseDto.ValidationErrors != null)
            {
                validatedEntity.Errors.AddRange(responseDto.ValidationErrors.Select(validationError => new ValidationError
                {
                    Message = validationError
                }));
            }
            return validatedEntity;
        }

        protected static IValidatedEntity GetValidatedEntityFromResponseDto(ValidatedDto validatedDto)
        {
            if (validatedDto == null)
                throw new ExternalApiException("Unable to get a response from endpoint");

            var validatedEntity = new ValidatedEntity();

            if (validatedDto.Success) return validatedEntity;

            var businessErrors = validatedDto.Errors.Where(error => error.IsBusinessError).ToList();

            if (businessErrors.Any())
            {
                var validationErrors = businessErrors.Select(error => new ValidationError()
                {
                    Message = error.Message,
                    InternalErrorCode = error.InternalErrorCode,
                    IsBusinessError = true

                });

                validatedEntity.Errors.AddRange(validationErrors);

                return validatedEntity;
            }

            throw new ExternalApiException(string.Join(", ", validatedDto.Errors));
        }
    }

    public class BaseAdapter<TEntity> : BaseAdapter
    {
        public BaseAdapter(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
        }

        internal static IValidatedEntity<TEntity> ValidateResponse<TResponseEntity>(ValidatedDto<TResponseEntity> responseDto)
        {
            if (responseDto == null)
                throw new ExternalApiException("Unable to get a response from endpoint");

            var validatedEntity = new ValidatedEntity<TEntity>();

            if (responseDto.Success) return validatedEntity;

            var businessErrors = responseDto.Errors.Where(error => error.IsBusinessError).ToList();

            if (businessErrors.Any())
            {
                var validationErrors = businessErrors.Select(error => new ValidationError()
                {
                    Message = error.Message,
                    InternalErrorCode = error.InternalErrorCode,
                    IsBusinessError = true
                     
                });

                validatedEntity.Errors.AddRange(validationErrors);

                return validatedEntity;
            }

            throw new ExternalApiException(string.Join(", ", responseDto.Errors));
        }

        internal static IValidatedEntity<TEntity> ValidateResponse<TResponseEntity>(ResponseDto<TResponseEntity> responseDto)
        {
            return ValidateResponseBase(responseDto);
        }

        internal static IValidatedEntity<TEntity> ValidateResponseBase(ResponseDto responseDto)
        {
            if (responseDto == null)
                throw new ExternalApiException("Unable to get a response from endpoint");

            if (!string.IsNullOrEmpty(responseDto.Error))
                throw new ExternalApiException(responseDto.Error);

            var validatedEntity = new ValidatedEntity<TEntity>();

            if (responseDto.ValidationErrors != null)
            {
                validatedEntity.Errors.AddRange(responseDto.ValidationErrors.Select(validationError => new ValidationError
                {
                    Message = validationError
                }));
            }
            return validatedEntity;
        }
    }
}
