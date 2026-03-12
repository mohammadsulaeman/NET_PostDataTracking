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
    }
}
