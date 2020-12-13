using FluentValidation;

namespace Pushinator.Web.Handlers.Auth
{
    public class RequestValidator: AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Credentials).NotNull();
            RuleFor(x => x.Credentials).SetValidator(new CredentialsValidator());
        }   
        
        private class CredentialsValidator: AbstractValidator<Credentials>
        {
            public CredentialsValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
    
    
}