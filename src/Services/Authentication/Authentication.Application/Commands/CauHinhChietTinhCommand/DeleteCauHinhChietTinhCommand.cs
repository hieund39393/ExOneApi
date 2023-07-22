﻿using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public record DeleteCauHinhChietTinhCommand(Guid id) : IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteCauHinhChietTinhCommand, bool> rồi implement
    public class DeleteCauHinhChietTinhCommandHandler : IRequestHandler<DeleteCauHinhChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteCauHinhChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }

        public async Task<bool> Handle(DeleteCauHinhChietTinhCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng CauHinhChietTinh không
            var entity = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.id).ToListAsync();
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Cấu hình"));
            }

            foreach (var item in entity)
            {
                item.IsDeleted = true; // xoá mềm 
                _unitOfWork.CauHinhChietTinhRepository.Update(item);
            }
            //xoá trong DB
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
