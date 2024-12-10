using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PZ_18.Models;

namespace PZ_18.Tests
{
    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void ChangePassword_UpdatesPasswordCorrectly()
        {
            var user = new Account
            {
                FullName = "Иванов Иван Иванович",
                PhoneNumber = "89999999999",
                Username = "ivanov",
                UserPassword = "oldpass",
                CategoryID = 1
            };

            user.ChangePassword("newHashedPass");

            Assert.That(user.UserPassword, Is.EqualTo("newHashedPass"), "Пароль должен обновиться на newHashedPass");
        }

        [Test]
        public void UpdateStatus_IfStatusIsReadyToGiveOut_SetCompletionDate()
        {
            var request = new RepairRequest
            {
                CreationDate = DateTime.Now.AddDays(-2),
                ApplianceID = 1,
                ApplianceModel = "ModelX",
                IssueDescription = "Не работает",
                Status = "В процессе ремонта",
                CustomerName = "Петров Петр Петрович",
                CustomerPhone = "89991234567"
            };

            request.UpdateStatus("Готова к выдаче");

            Assert.That(request.Status, Is.EqualTo("Готова к выдаче"), "Статус должен обновиться на 'Готова к выдаче'");
            Assert.That(request.ResolutionDate, Is.Not.Null, "ResolutionDate должен быть установлен");
        }

        [Test]
        public void RepairRequest_Validation_ShouldFailIfMissingRequiredFields()
        {
            var request = new RepairRequest
            {
                CreationDate = DateTime.Now,
                ApplianceID = 1,
                ApplianceModel = "ModelX",
                IssueDescription = "Не работает"
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

            Assert.That(isValid, Is.False, "Объект не должен быть валиден без обязательных полей");
            Assert.That(validationResults.Count, Is.GreaterThan(0), "Должны быть ошибки валидации");

            var invalidMembers = new List<string>();
            foreach (var vr in validationResults)
            {
                invalidMembers.AddRange(vr.MemberNames);
            }

            Assert.That(invalidMembers, Contains.Item("Status"));
            Assert.That(invalidMembers, Contains.Item("CustomerName"));
            Assert.That(invalidMembers, Contains.Item("CustomerPhone"));
        }

        [Test]
        public void RepairRequest_Validation_ShouldPassIfAllRequiredFieldsAreSet()
        {
            var request = new RepairRequest
            {
                CreationDate = DateTime.Now,
                ApplianceID = 1,
                ApplianceModel = "ModelX",
                IssueDescription = "Не работает",
                Status = "Новая заявка",
                CustomerName = "Иванов Иван Иванович",
                CustomerPhone = "89999999999"
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

            Assert.That(isValid, Is.True, "Объект должен быть валиден, так как все поля есть");
            Assert.That(validationResults.Count, Is.EqualTo(0), "Не должно быть ошибок валидации");
        }
    }
}
