using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AddAttachementTest
{
    [TestClass]
    public class AddAttachementTest
    {
        private TicketManager _ticketManager;
        private Project2Context _project2Context;

        [TestInitialize]
        public void Setup()
        {
            _project2Context = new Project2Context();
            _ticketManager = new TicketManager(_project2Context);
        }

        [TestMethod]
        public async Task EditTicketValidRequest()
        {
        }

    }
}
