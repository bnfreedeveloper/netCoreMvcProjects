using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageUploadRetrieve.Data;
using ImageUploadRetrieve.Models;
using System.IO;

namespace ImageUploadRetrieve.Controllers
{
    public class ImageController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnvironnement;

        public ImageController(DatabaseContext context, IWebHostEnvironment hostEnvironnement)
        {
            _context = context;
            _hostEnvironnement = hostEnvironnement;
        }

        // GET: Image
        public async Task<IActionResult> Index()
        {
              return View(await _context.Images.ToListAsync());
        }

        // GET: Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Title,ImageFile")] ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                // process for saving image to wwwrooot

                //we get the path of the wwwroot folder
                string wwwRootPath = _hostEnvironnement.WebRootPath;
                //we get the name of the file of IFormFile type
                var file = Path.GetFileName(imageModel.ImageFile.FileName);
                var fileNameWithoutExtention = file.Split(".")[0];

                //we get the extension of the image
                var fileNameExention = Path.GetExtension(imageModel.ImageFile.FileName);
                //i could check if the extension is included in a array of authorized extension
                //and return to view with message if not

                //we assure the unicity of the image Name by combining date and extension to the fileName
                var fileName = fileNameWithoutExtention + DateTime.Now.ToString("yymmssfff") + fileNameExention;
                imageModel.ImageName = fileName;    

                //we create the full path where to save the image
                string imagePath = Path.Combine(wwwRootPath +"/images/", fileName);

                //we transfert the file
                using (var filestream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageModel.ImageFile.CopyToAsync(filestream);
                }
                //now the part to save the image path to database

                _context.Add(imageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName,ImageFile")] ImageModel imageModel)
        {
            if (id != imageModel.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldImage = await _context.Images.FindAsync(id);
                    var webRootPath = _hostEnvironnement.WebRootPath;
                    if (oldImage != null)
                    {
                        if(oldImage.ImageName != imageModel.ImageName && imageModel.ImageFile != null)
                        {
                            var imagePath = Path.Combine(webRootPath,"images",oldImage.ImageName);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            var fileName = oldImage.ImageName;
                            var extention = Path.GetExtension(fileName);
                            
                            var newFileName =imageModel.ImageName ?? Path.GetFileName(imageModel.ImageFile.FileName).Split(".")[0];
                            var newName = newFileName + DateTime.Now.ToString("yymmssfff") + extention;
                            imageModel.ImageName = newName;
                            var newPathFile = Path.Combine(webRootPath + "/images/", newName);
                            using (var fileStream = new FileStream(newPathFile, FileMode.Create))
                            {
                                await imageModel.ImageFile.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            if(imageModel.ImageFile != null)
                            {
                                using (var fileStream = new FileStream(Path.Combine(webRootPath + "/images/", imageModel.ImageName), FileMode.Open))
                                {
                                    imageModel.ImageFile.CopyTo(fileStream);
                                }
                            }
                            
                        }
                        
                    }
                    if (imageModel.ImageFile != null)
                    {
                        oldImage.ImageFile = imageModel.ImageFile;  
                        oldImage.ImageName = imageModel.ImageName;
                        oldImage.Title= imageModel.Title;
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ImageModel imgModel)
        {
            //if (_context.Images == null)
            //{
            //    return Problem("Entity set 'DatabaseContext.Images'  is null.");
            //}
            var imageModel = await _context.Images.FindAsync(imgModel.ImageId) ;
            //delete image from wwwroot
           
            if (imageModel != null)
            {
                var imagePath = Path.Combine(_hostEnvironnement.WebRootPath, "images", imageModel.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.Images.Remove(imageModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageModelExists(int id)
        {
          return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
