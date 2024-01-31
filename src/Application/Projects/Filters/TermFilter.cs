using System.Text.RegularExpressions;
using CoduTeam.Domain.Entities;

namespace CoduTeam.Application.Projects.Filters;

public static class TermFilter
{
    public static IQueryable<Project> AddTermFilter(
        this IQueryable<Project> dbQuery,
        string? term)
    {
        var sanitizedTerm = GetSanitizedTerm(term ?? "");

        return string.IsNullOrEmpty(term)
            ? dbQuery
            : dbQuery.Where(x =>
                x.Description.Contains(term) ||
                x.Title.Contains(term) ||
                EF.Functions.Like(x.Category, $"%{sanitizedTerm}%"));
    }

    private static string GetSanitizedTerm(string term)
    {
        // Get only alphabetical characters
        return Regex.Replace(term.ToUpper().Trim(), "[^A-Za-z0-9 -]", "");
    }
}
