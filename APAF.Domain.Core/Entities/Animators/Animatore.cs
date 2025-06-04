using APAF.Domain.Core.Enums;
using APAF.Domain.Core.Entities.Cartoons;
using APAF.Domain.Core.Entities.Avatars;
using APAF.Domain.Core.Entities.RelAnimtoreCompetitions;
using APAF.Domain.Core.Entities.RelAssetbundelAnimatores;
using APAF.Domain.Core.Entities.VersionOfAPKs;
using APAF.Domain.Core.Entities.RelGifAnimatores;
using APAF.Domain.Core.Entities.Orders;
using APAF.Domain.Core.Entities.Schools;
using APAF.Domain.Core.Entities.Scores;

namespace APAF.Domain.Core.Entities.Animators
{
    public  class Animatore
    {
        #region  property
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName {get; set;}
        public string PhoneNumber { get; set;}
        public DateTime Register { get; set; }
        public DateTime?BrithDay{get; set; }
        public bool DoesItremove{get; set; }
        public Gender gender{ get; set; }
        public LevelOfStudy? levelOfStudy{ get; set; }
        public string ModelOfPhone {get; set;}
        public IranProvinces? Province{get; set; }
        public string City {get; set;}
        public string Address {get; set;}
        //age & Total Score are dynamic
        #endregion
        #region  Navigation property
        // (1-n) relationShipe between Avatar-Animatore
        public int FkAvatar {get; set;}
        public Avatar AvatarOfAnimatore;
        //(n-m) RelationShipe between Competition-Animatore
        public List<RelAnimtoreCompetition>CompetitionOfAnimatores{get; set; }
        //(n-m) RelationShipe between Assetbundel-Animatore
        public List<RelAssetbundelAnimatore>AssetbundelOfAnimatores{get; set; }
        //(1-m) RelationShipe between VersionOfAPK-Animatore
        public int FkIdversionOfAPK{get; set; }
        public VersionOfAPK VersionofAPK{get; set;}
        //(1-m) RelationShipe between Gift-Animatore
        public List<RelGifAnimatore>ListofAnimatoreOfGift {get; set;}
        //(1-m) RelationShipe between Order-Animatore
        public List<Order>ListOfOrder {get; set;}
        //(1-m) RelationShipe between School-Animatore
        public int? FKIDSchool {get; set;}
        public School SchoolOfAnimatore {get; set;}
        //(1-m) RelationShipe between Cartoon-Animatore
        public List<Cartoon>ListOfCartoon {get; set;}
        //(1-m) RelationShipe between Score-Animatore
        public List<Score>ListOfscoresAnimatore {get; set;}
        #endregion
        #region  Constructor
        public Animatore() { }
        public Animatore (string name,string lastname,string phonenumber,DateTime register,DateTime brithday,bool doesitremove, Gender gender,LevelOfStudy levelOfStudy,string modelofphone,IranProvinces? province,string city, string address){
            this.Name = name;
            this.LastName = lastname;
            this.PhoneNumber = phonenumber;
            this.Register = register;
            this.BrithDay=brithday;
            this.DoesItremove=doesitremove;
            this.gender = gender;
            this.levelOfStudy= levelOfStudy;
            this.ModelOfPhone = modelofphone;
            this.Province = province;
            this.City = city;   
            this.Address = address;
        }
        #endregion
    }
}
