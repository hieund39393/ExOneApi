﻿using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_LoaiCapConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_LoaiCap>
    {
        public void Configure(EntityTypeBuilder<DM_LoaiCap> builder)
        {
            builder.ToTable("DM_LoaiCap"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenLoaiCap).HasMaxLength(200); // Cấu hình độ dài tên Loại cáp
            builder.Property(x => x.MaLoaiCap).HasMaxLength(50); // Cấu hình độ dài mã  Loại cáp
            builder.Property(x => x.DonViTinh).HasMaxLength(50); // Cấu hình độ dài đơn vị tính
            ConfigureBase(builder);
        }
    }
}