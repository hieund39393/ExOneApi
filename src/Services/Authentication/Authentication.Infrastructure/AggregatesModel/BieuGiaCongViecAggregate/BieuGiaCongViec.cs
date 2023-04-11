﻿using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate
{
    public class BieuGiaCongViec: BaseEntity
    {
        // biểu giá
        public Guid? IdBieuGia { get; set; }
        public DM_BieuGia DM_BieuGias{ get; set; }
          
        // Công việc
        public Guid? IdCongViec { get; set; }
        public DM_CongViec DM_CongViecs { get; set; }
    }
}
