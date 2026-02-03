using System;

namespace PeopleSearchSite.Models
{
    // ViewModel для расширенного поиска
    public class SearchViewModel
    {
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string SortBy { get; set; } = "LastName"; // по умолчанию сортировка по фамилии
    }
}
