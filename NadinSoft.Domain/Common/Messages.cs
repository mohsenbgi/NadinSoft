namespace NadinSoft.Domain.Common
{

    public static class ErrorMessages
    {
        public const string MobileFormatError = "Mobile format is not correct";
        public const string EmailFormatError = "Email format is not correct";
        public const string DuplicateMobileError = "Mobile is duplicated";
        public const string ProcessFailedError = "Process failed";
        public const string ManufactureEmailIsDuplicatedError = "Manufacture email is duplicated";
        public const string ManufacturePhoneIsDuplicatedError = "Manufacture phone is duplicated";
        public const string ItemNotFoundError = "Item not found";
        public const string YouCanNotEditProductError = "You can not edit the product";
    }


    public static class SuccessMessages
    {
        public const string SuccessfullyDone = "Successfully done";
        public const string SuccessfullyUpdated = "Successfully updated";
        public const string SuccessfullyDeleted = "Successfully deleted";
    }
}
