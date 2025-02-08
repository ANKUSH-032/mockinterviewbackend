using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockinterview.core.Model
{
    public class Candidate
    {

        #region Entity Properties


        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]

        public string? FirstName { get; set; }

        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]

        public string? LastName { get; set; }

        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? ContactNo { get; set; }

        [Required, StringLength(150, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]

        public string? Email { get; set; }

        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? RoleID { get; set; }
        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? Address { get; set; }
        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? HospitalName { get; set; }

        public string? ZipCode { get; set; }
        public string? Gender { get; set; }


        #endregion Entity Properties

        #region Helper Properties
        #endregion Helper Properties
    }
    public class CandidateInsert : Candidate
    {
        public string? Password { get; set; }
        public string? CreatedBy { get; set; }

        [DataType(DataType.Password)]
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

    }


    public class CandidateUpdate : Candidate
    {
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? CandidateId { get; set; }
        public string? UpdatedBy { get; set; }
    }


    public class CandidateGetDetails : Candidate
    {
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? CandidateId { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? RoleName { get; set; }
    }


    public class DeleteCandidate
    {
        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? CandidateId { get; set; }
        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? DeletedBy { get; set; }

    }
    public class GetCandidate
    {
        [Required, StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string? CandidateId { get; set; }
    }
    public class CandidateList
    {
        #region Entity Properties
        public string? CandidateId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? RoleID { get; set; }
        public string? Address { get; set; }
        public string? HospitalName { get; set; }
        public string? ZipCode { get; set; }
        public string? Gender { get; set; }


        #endregion Entity Properties

        #region Helper Properties
        #endregion Helper Properties
    }
}

