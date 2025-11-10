using as_webApp_helloWorld.DataModels;
using as_webApp_helloWorld.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;

namespace as_webApp_helloWorld.Controllers
{
    public class AttendeeRegistrationController : Controller
    {
        private readonly ITableStorageService tableStorageService;

        public AttendeeRegistrationController(ITableStorageService tableStorageService)
        {
            this.tableStorageService = tableStorageService;
        }

        // GET: AttendeeRegistrationController
        public async Task<ActionResult> Index()
        {
            var data = await this.tableStorageService.GetAttendeeEntitys();
            return View(data);
        }

        // GET: AttendeeRegistrationController/Details/5
        public async Task<ActionResult> Details(string partitionKey, string rowKey)
        {
            var data = await this.tableStorageService.GetAttendeeEntity(partitionKey, rowKey);
            return View(data);
        }

        // GET: AttendeeRegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttendeeRegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AttendeeEntity attendeeEntity)
        {
            try
            {
                attendeeEntity.PartitionKey = attendeeEntity.EmailAddress;
                attendeeEntity.RowKey = Guid.NewGuid().ToString();
                await this.tableStorageService.UpdateAttendee(attendeeEntity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AttendeeRegistrationController/Edit/5
        public async Task<ActionResult> Edit(string partitionKey, string rowId)
        {
            var data = await this.tableStorageService.GetAttendeeEntity(partitionKey, rowId);
            return View(data);
        }

        // POST: AttendeeRegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AttendeeEntity attendeeEntity)
        {
            try
            {
                attendeeEntity.PartitionKey = attendeeEntity.EmailAddress;
                await this.tableStorageService.UpdateAttendee(attendeeEntity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: AttendeeRegistrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string partitionKey, string rowId)
        {
            try
            {
                await this.tableStorageService.DeleteAttendee(partitionKey, rowId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
