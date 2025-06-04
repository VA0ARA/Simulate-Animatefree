using APAF.Domain.Core.Entities.Animators;
using APAF.Domain.Core.Entities.AssetBundels;
using APAF.Domain.Core.Entities.Avatars;
using APAF.Domain.Core.Entities.BannerOfAssets;
using APAF.Domain.Core.Entities.Cartoons;
using APAF.Domain.Core.Entities.Competitions;
using APAF.Domain.Core.Entities.Gifts;
using APAF.Domain.Core.Entities.IdentityEntities;
using APAF.Domain.Core.Entities.Orders;
using APAF.Domain.Core.Entities.RelAnimtoreCompetitions;
using APAF.Domain.Core.Entities.RelAseetBundelCompetitions;
using APAF.Domain.Core.Entities.RelAssetbundelAnimatores;
using APAF.Domain.Core.Entities.RelGifAnimatores;
using APAF.Domain.Core.Entities.Schools;
using APAF.Domain.Core.Entities.Scores;
using APAF.Domain.Core.Entities.VersionOfAPKs;
using APAF.Infrastructure.EFCore.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace APAF.Infrastructure.EFCore.Common;
public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region EntityConfiguation
        modelBuilder.ApplyConfiguration(new ScoreConfiguration());
        modelBuilder.ApplyConfiguration(new AnimatoreConfiguration());
        modelBuilder.ApplyConfiguration(new AssetBundelConfiguration());
        modelBuilder.ApplyConfiguration(new AvatarConfiguration());
        modelBuilder.ApplyConfiguration(new BannerOfAssetConfiguration());
        modelBuilder.ApplyConfiguration(new CartoonConfiguration());
        modelBuilder.ApplyConfiguration(new GiftConfiguration());
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new RelAnimatoreAssetBundelConfiguration());
        modelBuilder.ApplyConfiguration(new RelAnimatoreCompetitionConfiguration());
        modelBuilder.ApplyConfiguration(new RelAseetBundelCompetitionConfiguration());
        modelBuilder.ApplyConfiguration(new RelGiftAnimatoreConfiguration());
        modelBuilder.ApplyConfiguration(new SchoolConfiguration());
        modelBuilder.ApplyConfiguration(new VersionAPKConfiguration());
        #endregion
        base.OnModelCreating(modelBuilder);
    }
    #region  Tables
    //14
    // I have to remove This code due to NDA Contract with company 
    #endregion
}
