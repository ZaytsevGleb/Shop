using System.Text;
using FluentValidation.Results;

namespace Shop.Services.Catalog.BusinessLogic.Common.Exceptions;

public class ValidationException : Exception
{
    public string FormattedMessage { get; }
    public IDictionary<string, string[]> Failures { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
        FormattedMessage = GetFormattedMessage();
    }

    public ValidationException(List<ValidationFailure> failures)
        : this()
    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }

        FormattedMessage = GetFormattedMessage();
    }

    private string GetFormattedMessage()
    {
        var message = new StringBuilder();
        foreach (var key in Failures.Keys)
        {
            if (!string.IsNullOrEmpty(key))
            {
                message.Append(key + ":");
            }

            var first = true;
            foreach (var value in Failures[key])
            {
                message.Append(first ? string.IsNullOrEmpty(key) ? string.Empty : " " : ", ");
                message.Append(value);
                first = false;
            }

            message.AppendLine();
        }

        return message.ToString().TrimEnd('\r', '\n');
    }
}