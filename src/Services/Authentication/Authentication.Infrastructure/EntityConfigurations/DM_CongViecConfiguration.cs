﻿using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_CongViecConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_CongViec>
    {
        public void Configure(EntityTypeBuilder<DM_CongViec> builder)
        {
            builder.ToTable("DM_CongViec"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenCongViec).HasMaxLength(200); // Cấu hình độ dài tên 
            builder.Property(x => x.MaCongViec).HasMaxLength(20); // Cấu hình độ dài mã biểu giá
            builder.Property(x => x.DonViTinh).HasMaxLength(50); // Cấu hình độ dài đơn vị tính
            ConfigureBase(builder);
        }
    }
}