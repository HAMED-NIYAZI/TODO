namespace LawyerCoreApp.Domain.Shared
{
    public static class ErrorMessages
    {
        public const string RequiredError = "RequiredError";
        public const string MaxLengthError = "Error.MaxLength";
        public const string MinLengthError = "Error.MinLength";
        public const string MinLenMobileError = "MinLenMobileError";
        public const string MaxLenMobileError = "MaxLenMobileError";
        public const string CompareError = "Error.Compare";
        public const string MobileFormatError = "MobileFormatError";
        public const string EmailFormatError = "Error.EmailFormat";
        public const string DuplicateEmailError = "Error.EmailDuplicate";
        public const string DuplicateMobileError = "Error.MobileDuplicate";
        public const string PriceRangeError = "Error.PriceRange";
        public const string NullValue = "Error.NullValue";
        public const string ShouldBeNumber = "ShouldBeNumber";
        public const string SlugError = "Error.SlugError";
        public const string MinimumError = "Error.MinimumError";
        public const string EnglishWordsOnly = "Error.EnglishWordsOnly";
        public const string ImageRequired = "Error.ImageRequired";
    }

    public static class SuccessMessages
    {
        public const string UserDeletedSuccessfully = "Success.UserDeletedSuccessfully";
        public const string UserCreatedSuccessfully = "Success.UserCreatedSuccessfully";
        public const string UserUpdatedSuccessfully = "Success.UserUpdatedSuccessfully";
    }
}
