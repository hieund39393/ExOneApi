﻿using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaTongHopCommand
{
    public class UpdateBieuGiaTongHopCommand : IRequest<bool>
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int TinhTrang { get; set; }
        public string GhiChu { get; set; }
        public IFormFile File { get; set; }
    }
    public class UpdateBieuGiaTongHopCommandHandler : IRequestHandler<UpdateBieuGiaTongHopCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdateBieuGiaTongHopCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool> Handle(UpdateBieuGiaTongHopCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());
            var data = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy && x.TinhTrang == request.TinhTrang).ToListAsync();
            if (!data.Any())
            {
                throw new EvnException("Không có dữ liệu");
            }

            foreach (var item in data)
            {
                item.TinhTrang = request.TinhTrang + 1;

                if (item.TinhTrang >= 2)
                {
                    item.NguoiXacNhan = userId;
                    item.NgayXacNhan = DateTime.Now;
                }
                _unitOfWork.BieuGiaTongHopRepository.Update(item);
            }

            var chiTiet = new BieuGiaTongHopChiTiet();
            chiTiet.Nam = request.Nam;
            chiTiet.Quy = request.Quy;
            chiTiet.GhiChu = request.GhiChu;
            if (request.File != null && request.File.Length > 0)
            {
                string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath + "/VanBan");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                string filePath = Path.Combine(uploadDirectory, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(fileStream);
                }
                chiTiet.VanBan = $"/VanBan/{uniqueFileName}";
            }
            _unitOfWork.BieuGiaTongHopChiTietRepository.Add(chiTiet);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
