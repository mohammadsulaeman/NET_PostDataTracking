using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ad ads)
        {
            try
            {
                db.Ads.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AdsDTO> GetAds()
        {
            List<AdsDTO> adsList = new List<AdsDTO>();
            List<Ad> adList = db.Ads.Where(x => x.isDeleted == false).ToList();
            foreach(var item in adList)
            {
                AdsDTO dto = new AdsDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dto.ImageSize = item.Size;
                adsList.Add(dto);
            }

            return adsList;
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ad ads = db.Ads.First(x => x.ID == model.ID);
                string oldImagePath = ads.ImagePath;
                ads.Name = model.Name;
                ads.Link = model.Link;
                if (model.ImagePath != null)
                    ads.ImagePath = model.ImagePath;
                ads.Size = model.ImageSize;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }catch(Exception e)
            {
                throw e;
            }
        }

        public AdsDTO GetAdsWithID(int iD)
        {
            AdsDTO dto = new AdsDTO();
            Ad ad = db.Ads.First(x => x.ID == iD);
            dto.ID = ad.ID;
            dto.Name = ad.Name;
            dto.ImagePath = ad.ImagePath;
            dto.Link = ad.Link;
            dto.ImageSize = ad.Size;
            return dto;
        }
    }
}
