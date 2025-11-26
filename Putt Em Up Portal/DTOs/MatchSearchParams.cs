using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Putt_Em_Up_Portal.DTOs
{
    public class MatchSearchParams
    {

       
        public DateTime StartDate {get; set;}

       
        public SearchMode Mode { get;set;}
    }
}
