//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Autofac;
//using Harvest.OrchardDevToolbelt.Models;
//using Harvest.OrchardDevToolbelt.Services;
//using NUnit.Framework;
//using Orchard.Caching;
//using Orchard.Data;
//using Orchard.Tests;
//using Orchard.Tests.Stubs;
//using Orchard.UI.Notify;

//namespace Harvest.OrchardDevToolbelt.Tests.Services {
//    [TestFixture]
//    public class ContactFormServiceTests : DatabaseEnabledTestsBase {
//        [Test]
//        public void AddingAnEntrySignalsTheCache() {

//            // Arrange
//            var repository = _container.Resolve<IRepository<ContactFormEntry>>();
//            var service = _container.Resolve<IContactFormService>();

//            repository.Create(new ContactFormEntry { CreatedUtc = new DateTime(2012, 1, 1), MessageBody = "Message 1", Email =  "email1@domain.com", Name = "Name 1", Subject = "Subject 1"});
//            repository.Create(new ContactFormEntry { CreatedUtc = new DateTime(2012, 1, 2), MessageBody = "Message 2", Email = "email2@domain.com", Name = "Name 2", Subject = "Subject 2" });
//            repository.Create(new ContactFormEntry { CreatedUtc = new DateTime(2012, 1, 3), MessageBody = "Message 3", Email = "email3@domain.com", Name = "Name 3", Subject = "Subject 3" });

//            // Act
//            var entries = service.GetEntries();
//            var cachedEntries = service.GetEntries();
//            service.StoreMessage(new ContactFormEntry { CreatedUtc = new DateTime(2012, 1, 4), MessageBody = "Message 4", Email = "email4@domain.com", Name = "Name 4", Subject = "Subject 4" });
//            var newListOfEntries = service.GetEntries();

//            // Assert
//            Assert.AreSame(entries, cachedEntries);
//            Assert.That(newListOfEntries.Count() == 4);
//        }

//        public override void Register(ContainerBuilder builder) {
//            builder.RegisterType<StubCacheManager>().As<ICacheManager>();
//            builder.RegisterType<Signals>().As<ISignals>();
//            builder.RegisterType<Notifier>().As<INotifier>();
//            builder.RegisterType<ContactFormService>().As<IContactFormService>();
//        }

//        protected override IEnumerable<Type> DatabaseTypes {
//            get {
//                return new[] {
//                    typeof(ContactFormEntry),
//                };
//            }
//        }
//    }
//}