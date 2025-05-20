namespace Domain.Order.Suppport.Vlaidators;

public static class ClientDetailsValidator
{
    public static void Validate(
        string clientFullName,
        string clientPhoneNumber,
        string countryForDelivery,
        string city,
        string email)
    {
        if (string.IsNullOrWhiteSpace(clientFullName))
            throw new ArgumentException("Full name is required.", nameof(clientFullName));

        if (string.IsNullOrWhiteSpace(clientPhoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(clientPhoneNumber));

        if (string.IsNullOrWhiteSpace(countryForDelivery))
            throw new ArgumentException("Country for delivery is required.", nameof(countryForDelivery));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.", nameof(city));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (!IsValidEmail(email))
            throw new ArgumentException("Email is not valid.", nameof(email));

        if (!IsValidPhoneNumber(clientPhoneNumber))
            throw new ArgumentException("Phone number is not valid.", nameof(clientPhoneNumber));
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ' || c == '(' || c == ')');
    }
}