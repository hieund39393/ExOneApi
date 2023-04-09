﻿using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class ChiTietBieuGiaConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<ChiTietBieuGia>
    {
        public void Configure(EntityTypeBuilder<ChiTietBieuGia> builder)
        {
            builder.ToTable("ChiTietBieuGia"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.SoLuong).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.HeSoDieuChinh_K1nc).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.HeSoDieuChinh_K2nc).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.HeSoDieuChinh_K2mnc).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.DonGia_VL).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.DonGia_NC).HasColumnType("numeric(18,1)");;
            builder.Property(x => x.DonGia_MTC).HasColumnType("numeric(18,1)"); ;
            builder.HasOne(x => x.DM_BieuGia).WithMany(x => x.ChiTietBieuGia).HasForeignKey(x => x.DM_BieuGiaID); // cấu hình 1-N loại danh mục biểu giá
            //builder.HasOne(x => x.DM_Vung).WithMany(x => x.ChiTietBieuGia).HasForeignKey(x => x.VungID); // cấu hình 1-N loại vùng
            //builder.HasOne(x => x.DM_KhuVuc).WithMany(x => x.ChiTietBieuGia).HasForeignKey(x => x.KhuVucID); // cấu hình 1-N loại khu vực
            //builder.HasOne(x => x.DM_CongViec).WithMany(x => x.ChiTietBieuGia).HasForeignKey(x => x.CongViecID); // cấu hình 1-N công việc
            ConfigureBase(builder);
        }
    }
}
