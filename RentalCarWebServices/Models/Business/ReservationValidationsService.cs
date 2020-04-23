using RentalCarWebServices.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace RentalCarWebServices.Models.Business
{
    public class ReservationValidationsService
    {
        private static readonly ReservationValidationsService instance = new ReservationValidationsService();
        static ReservationValidationsService()
        {
        }
        private ReservationValidationsService()
        {
        }
        public static ReservationValidationsService Instance
        {
            get
            {
                return instance;
            }
        }

        private static ReservationService reservationService = ReservationService.Instance;
        public bool validateDate(DateTime startDate, DateTime endDate)
        {
            bool date = true;
            if (startDate > endDate)
            {
                date = false;
            }
            return date;
        }

        public bool validateRentPeriod(string plate, DateTime presentStartDate, DateTime presentEndDate, string condition, Reservation reservation)
        {
            bool selectedDate = true;
            DateTime startDate;
            DateTime endDate;
            int rStatus;
            DataTable dt;
            Reservation dbR = new Reservation();

            dt = reservationService.readByPlate(plate);

            foreach (DataRow row in dt.Rows)
            {
                dbR.carPlate = row["CarPlate"].ToString();
                dbR.costumerID = Int32.Parse(row["CostumerID"].ToString());
                dbR.startDate = DateTime.Parse(row["StartDate"].ToString());
                dbR.carID = Int32.Parse(row["CarID"].ToString());
                dbR.reservStatsID = Int32.Parse(row["ReservStatsID"].ToString());
                dbR.endDate = DateTime.Parse(row["EndDate"].ToString());
                dbR.location = row["Location"].ToString();
                dbR.couponCode = row["CouponCode"].ToString();

                startDate = DateTime.Parse(row["StartDate"].ToString());
                endDate = DateTime.Parse(row["EndDate"].ToString());
                rStatus = Int32.Parse(row["ReservStatsID"].ToString());

                if (((presentStartDate <= endDate && presentEndDate >= startDate) && condition == "INSERT" && rStatus == 1) || (((presentStartDate <= endDate && presentEndDate >= startDate) && !reservation.Equals(dbR)) && condition == "UPDATE" && rStatus == 1))
                {
                    selectedDate = false;
                    break;
                }
            }
            return selectedDate;
        }
    }
}