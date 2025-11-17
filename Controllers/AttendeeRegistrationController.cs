using as_webApp_helloWorld.DataModels;
using as_webApp_helloWorld.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace as_webApp_helloWorld.Controllers
{
    public class AttendeeRegistrationController : Controller
    {
        private readonly ITableStorageService tableStorageService;
        private readonly IBlobStorageService blobStorageService;

        public AttendeeRegistrationController(ITableStorageService tableStorageService, IBlobStorageService blobStorageService)
        {
            this.tableStorageService = tableStorageService;
            this.blobStorageService = blobStorageService;
        }

        // GET: AttendeeRegistrationController
        public async Task<ActionResult> Index()
        {
            var data = await this.tableStorageService.GetAttendeeEntitys();
            foreach (var entity in data) 
            {
                entity.ProfileImage = await blobStorageService.GetBlobUrl(entity.ProfileImage);
            }

            return View(data);
        }

        // GET: AttendeeRegistrationController/Details/5
        public async Task<ActionResult> Details(string partitionKey, string rowKey)
        {
            var data = await this.tableStorageService.GetAttendeeEntity(partitionKey, rowKey);
            data.ProfileImage = await blobStorageService.GetBlobUrl(data.ProfileImage);
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
        public async Task<ActionResult> Create(AttendeeEntity attendeeEntity, IFormFile formFile )
        {
            try
            {
                attendeeEntity.PartitionKey = attendeeEntity.EmailAddress;
                attendeeEntity.RowKey = Guid.NewGuid().ToString();
                

                if (formFile.Length > 0) 
                {
                    attendeeEntity.ProfileImage = await blobStorageService.UploadBlobFile(formFile, attendeeEntity.RowKey); 
                }
                else
                {
                    attendeeEntity.ProfileImage = "default.jpg";
                }

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
        public async Task<ActionResult> Edit(AttendeeEntity attendeeEntity, IFormFile formFile)
        {
            try
            {
                attendeeEntity.PartitionKey = attendeeEntity.EmailAddress;

                if (formFile?.Length > 0)
                {
                    attendeeEntity.ProfileImage = await blobStorageService.UploadBlobFile(formFile, attendeeEntity.RowKey);
                }

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
                var data = await this.tableStorageService.GetAttendeeEntity(partitionKey, rowId);
                await this.tableStorageService.DeleteAttendee(partitionKey, rowId);
                await this.blobStorageService.RemoveBlob(data.ProfileImage);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
