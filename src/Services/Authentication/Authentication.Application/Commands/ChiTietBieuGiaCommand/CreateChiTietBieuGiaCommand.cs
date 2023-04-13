﻿using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class CreateChiTietBieuGiaCommand : IRequest<bool>
    {
        public Guid? IdBieuGia { get; set; }
        public Guid? IdCongViec { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public decimal SoLuong { get; set; }
        public decimal HeSoDieuChinh_K1nc { get; set; }
        public decimal HeSoDieuChinh_K2nc { get; set; }
        public decimal HeSoDieuChinh_K2mnc { get; set; }
        public decimal DonGia_VL { get; set; }
        public decimal DonGia_NC { get; set; }
        public decimal DonGia_MTC { get; set; }
    }
    public class CreateChiTietBieuGiaCommandHandler : IRequestHandler<CreateChiTietBieuGiaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateChiTietBieuGiaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CreateChiTietBieuGiaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ChiTietBieuGiaRepository.FindOneAsync(x => x.IDBieuGia == request.IdBieuGia & x.IDCongViec == request.IdCongViec);

            if (entity == null)
            {
                var model = new ChiTietBieuGia
                {
                    IDBieuGia = request.IdBieuGia,
                    IDCongViec = request.IdCongViec,
                    Nam = request.Nam,
                    Quy = request.Quy,
                    SoLuong = request.SoLuong,
                    HeSoDieuChinh_K1nc = request.HeSoDieuChinh_K1nc,
                    HeSoDieuChinh_K2nc = request.HeSoDieuChinh_K2nc,
                    HeSoDieuChinh_K2mnc = request.HeSoDieuChinh_K2mnc,
                    DonGia_VL = request.DonGia_VL,
                    DonGia_NC = request.DonGia_NC,
                    DonGia_MTC = request.DonGia_MTC,
                };
                //thêm vào DB
                _unitOfWork.ChiTietBieuGiaRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Chi tiết biểu giá"));
        }
    }
}