namespace SampleBookingSystem.Common
{
    public static class GlobalConstants
    {
        public const string AdministratorRoleName = "Admin";

        public const string ClientRoleName = "Client";

        public const string CheckinDateErrorMessage = "The chekin date must be equal to or greater than today's date";

        public const string CheckoutDateErrorMessage = "The checkout date must be greater than the checkin date";

        public const string MissingRoomIdMessage = "You have to select a room to make a reservation";
    }
}
