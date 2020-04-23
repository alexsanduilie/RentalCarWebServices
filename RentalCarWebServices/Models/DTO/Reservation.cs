using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.DTO
{
    public class Reservation
    {
        public int carID { get; set; }
        public string carPlate { get; set; }
        public int costumerID { get; set; }
        public int reservStatsID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string location { get; set; }
        public string couponCode { get; set; }

        public Reservation(int carID, string carPlate, int costumerID, int reservStatsID, DateTime startDate, DateTime endDate, string location, string couponCode)
        {
            this.carID = carID;
            this.carPlate = carPlate;
            this.costumerID = costumerID;
            this.reservStatsID = reservStatsID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.location = location;
            this.couponCode = couponCode;
        }

        public Reservation() { }

        public override string ToString()
        {
            return String.Format("Car ID:{0}, Car Plate:{1}, Costumer ID:{2}, Reservation Status:{3}, Start Date:{4}, End Date:{5}, Location:{6}, Coupon Code:{7}", carID, carPlate, costumerID, reservStatsID, startDate, endDate, location, couponCode);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Reservation;

            if (other == null)
                return false;

            if (carID != other.carID || carPlate != other.carPlate || costumerID != other.costumerID || reservStatsID != other.reservStatsID || startDate != other.startDate || endDate != other.endDate || location != other.location || couponCode != other.couponCode)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = -593334014;
            hashCode = hashCode * -1521134295 + carID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(carPlate);
            hashCode = hashCode * -1521134295 + costumerID.GetHashCode();
            hashCode = hashCode * -1521134295 + reservStatsID.GetHashCode();
            hashCode = hashCode * -1521134295 + startDate.GetHashCode();
            hashCode = hashCode * -1521134295 + endDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(location);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(couponCode);
            return hashCode;
        }
    }
}