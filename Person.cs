// Модель Person описывает человека в нашей базе данных
// Каждый объект этого класса = одна строка в таблице БД

using System;

namespace PeopleSearchSite.Models
{
    public class Person
    {
        // Уникальный идентификатор человека (Primary Key)
        public int Id { get; set; }

        // Фамилия
        public string LastName { get; set; } = string.Empty;

        // Имя
        public string FirstName { get; set; } = string.Empty;

        // Отчество
        public string MiddleName { get; set; } = string.Empty;

        // Дата рождения
        public DateTime BirthDate { get; set; }

        // Путь к файлу с фотографией
        // Пример: "/images/people/ivanov.jpg"
        public string PhotoPath { get; set; } = string.Empty;
    }
}
