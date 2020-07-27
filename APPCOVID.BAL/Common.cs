using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace APPCOVID.BAL
{
    public static class CommonHelper
    {
        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }
        public enum UserTypeEnum
        {
            admin = 1,
            customer = 2,
            citizen = 3
        }






        //public enum Disease { 
        // q1= "dry cough or sore throat",

        //}

    }

}

namespace APPCOVID.BAL
{
    public class InfectionSeverityCodesModel
    {
        public string InfectionQuestion { get; set; }
        public string InfectionDesc { get; set; }
        public int SeverityCode { get; set; }
        public bool Answer { get; set; }
    }
    public class InfectionsRepo
    {
        public static List<InfectionSeverityCodesModel> InfectionsSeverityList
        {
            get
            {
                return new List<InfectionSeverityCodesModel>() {
            new InfectionSeverityCodesModel{ InfectionQuestion = "q1", InfectionDesc="Dry Cough or Sore Throat", SeverityCode=2, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q2", InfectionDesc="Fever", SeverityCode=3, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q3", InfectionDesc="Trouble in breathing or Short of breath", SeverityCode=1, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q4", InfectionDesc="Fatigue, Aches and Pains", SeverityCode=6, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q5", InfectionDesc="Loss of Taste or Smell", SeverityCode=4, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q6", InfectionDesc="Chest Pain or Feeling Pressure in your Chest Region", SeverityCode=6, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q7", InfectionDesc="Conjunctivitis or Headache", SeverityCode=1, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q8", InfectionDesc="Rashes on your skin or Discoloration of fingers or toes", SeverityCode=2, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q9", InfectionDesc="Diarrhoea", SeverityCode=2, Answer=false},
            new InfectionSeverityCodesModel{ InfectionQuestion = "q10", InfectionDesc="Loss of Speech or Movement", SeverityCode=3, Answer=false}
        };
            }
        }
    }
}