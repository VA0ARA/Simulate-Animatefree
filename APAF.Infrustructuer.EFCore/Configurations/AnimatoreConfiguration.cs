using APAF.Domain.Core.Entities.Animators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APAF.Domain.Core.Enums;
using APAF.Domain.Core.Entities.Avatars;

namespace APAF.Infrastructure.EFCore.Configurations
{
    public class AnimatoreConfiguration : IEntityTypeConfiguration<Animatore>
    {
            public void Configure(EntityTypeBuilder<Animatore>builder){
            #region Customize table
            #region  property
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();

            builder.Property(e=>e.Name)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnType("nvarchar(15)");

            builder.Property(e=>e.LastName)
            .IsRequired(false)
            .HasMaxLength(15)
            .HasColumnType("nvarchar(15)");

            builder.Property(e=>e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnType("nvarchar(11)")
            .IsFixedLength();
            builder.HasIndex(t => t.PhoneNumber).IsUnique();

            builder.Property(e=>e.Register)
            .IsRequired()
            .HasColumnType("datetime2");

            builder.Property(e=>e.BrithDay)
            .IsRequired(false)
            .HasColumnType("date");

            builder.Property(e=>e.DoesItremove)
            .IsRequired()
            .HasColumnType("bit");

            builder.Property(e => e.gender)
            .IsRequired()               
            .HasConversion<int>()        
            .HasColumnType("int");      

            builder.Property(e => e.levelOfStudy)
            .IsRequired(false)               
            .HasConversion<int>()        
            .HasColumnType("int");

            builder.Property(e=>e.ModelOfPhone)
            .HasColumnType("nvarchar(60)")
            .IsRequired(false);

            builder.Property(e => e.Province)
            .IsRequired(false)               
            .HasConversion<int>()        
            .HasColumnType("int");

            builder.Property(e=>e.City)
            .HasColumnType("nvarchar(60)")
            .IsRequired(false) ;

            builder.Property(e=>e.Address)
            .HasColumnType("nvarchar(200)")
            .IsRequired(false) ;

            //builder.Property(e=>e.FkAvatar)
            //.IsRequired();
            #endregion
            #region Navigation property
            #region 1.(1-n) relationShipe between Avatar-Animatore
                builder.HasOne(x => x.AvatarOfAnimatore)
                .WithMany(x => x.ListOfanimAtore)
                .HasForeignKey(x => x.FkAvatar)
                .OnDelete(DeleteBehavior.Restrict);
                builder.Property(a=>a.FkAvatar)
                .HasDefaultValue(1);
            #endregion
            //2.(n-m) RelationShipe between Competition-Animatore
            // in the relAnimatoreMatch
            // 3.(n-m) RelationShipe between Assetbundel-Animatore
            //in the relAnimatoreAssetbundel
            //4.(1-m) RelationShipe between Gift-Animatore
            //in RelGiftAnimatore
            #region 5.(1-m) RelationShipe between Order-Animatore
            builder.HasMany(x => x.ListOfOrder).WithOne(x => x.AnimatoreOfOrder)
            .HasForeignKey(x=>x.FkIDAnimatore)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region 5.(1-m) RelationShipe between Score-Animatore
            builder.HasMany(x => x.ListOfscoresAnimatore).WithOne(x => x.AnimatoreOfScore )
            .HasForeignKey(x=>x.IdAnimatore)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region 6.(1-m) RelationShipe between School-Animatore
                builder.HasOne(x => x.SchoolOfAnimatore)
                .WithMany(x => x.ListOfAnimatores)
                .HasForeignKey(x => x.FKIDSchool)
                .OnDelete(DeleteBehavior.Restrict);
                builder.Property(x => x.FKIDSchool)
                .IsRequired(false);
                #endregion
            #region 7.(1-m) RelationShipe between Cartoon-Animatore
            builder.HasMany(x=>x.ListOfCartoon)
            .WithOne(x => x.AnimatoreOfCartoon)
            .HasForeignKey(x=>x.FKIdAnimatore)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
            #endregion
            #region  8.(1-m) RelationShipe between Version Of APK and Animatore
            builder.HasOne(e=>e.VersionofAPK)
            .WithMany(e=>e.ListOfAnimatore)
            .HasForeignKey(e=>e.FkIdversionOfAPK)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            #endregion
            #endregion
            #endregion
        }
    }
}
