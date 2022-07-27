using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Place.Common.Enums
{

    public enum CustomerStatus : byte
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Failed")]
        Failed,
        [Display(Name = "Success")]
        Success,
    }
    public enum PaymentType : byte
    {
        [Display(Name = "")]
        Active,
        [Display(Name = "")]
        Failed,
        [Display(Name = "")]
        Success,
    }

    public enum UserActivityType : byte
    {
        Null,
        Signin,
        Signout,
        Signup,
        Verify,
        OpenIssue,
        ChangeIssueStatus,
        GetIssuesList,
        GetAllIssuesList,
        GetIssueById,
        AddReview
    }
    public enum UserActivityStatus : byte
    {
        [Display(Name = "Info")]
        Info,
        [Display(Name = "Failed")]
        Failed,
        [Display(Name = "Success")]
        Success,
    }

    public enum StepStatus : byte
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "Accepted")]
        Accepted
    }

    public enum SubjectFieldType : byte
    {
        [Display(Name = "Text")]
        Text,
        [Display(Name = "Long")]
        Long,
        [Display(Name = "Double")]
        Double,
        [Display(Name = "Dropdown")]
        Dropdown,
        [Display(Name = "RadioButton")]
        RadioButton,
        [Display(Name = "CheckBox")]
        CheckBox,
        [Display(Name = "Date")]
        Date,
        [Display(Name = "Time")]
        Time,
        [Display(Name = "DateTime")]
        DateTime,
    }
    public enum AccountType : byte
    {
        [Display(Name = "SuperAdmin")]
        SuperAdmin,
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Agent")]
        Agent,
        [Display(Name = "SaleManager")]
        SaleManager,
        [Display(Name = "AllType")]
        AllType,
    }

    public enum IssueNotificationStatus : byte
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "Seen")]
        Seen,
        [Display(Name = "ChangedState")]
        ChangedState,
        [Display(Name = "Closed")]
        Closed
    }

    public enum ClientType : byte
    {
        Web,
        Android,
        IOS,
        WindowsPhone,
        DirectApi,
        Landing
    }
    public enum IssueStatus : byte
    {
        [Display(Name = "در انتظار بررسی")]
        Pending,
        [Display(Name = "در حال بررسی")]
        InProgress,
        [Display(Name = "رد شده")]
        Rejected,
        [Display(Name = "انجام شده")]
        Done,
        [Display(Name = "آرشیو شده")]
        Archived,
        [Display(Name = "نامعتبر")]
        Invalid,
        [Display(Name = "دیده نشده")]
        NotSeen
    }

    public enum NotificationType : byte
    {
        [Display(Name = "Default")]
        Default, //read from setting
        [Display(Name = "InApp")]
        InApp,
        [Display(Name = "VoiceCall")]
        VoiceCall,
        [Display(Name = "Sms")]
        Sms,
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Telegram")]
        Telegram,
        [Display(Name = "Whatsapp")]
        Whatsapp,
        [Display(Name = "Instagram")]
        Instagram,
    }
    public enum FileAttachmentType : byte
    {
        [Display(Name = "Document")]
        Document = 1,
        [Display(Name = "Circular")]
        Circular = 2,
        [Display(Name = "Instructions")]
        Instructions = 3,
    }
    public enum Education : byte
    {
        [Display(Name = "Unknown")]
        Unknown = 1,
        [Display(Name = "HighSchool")]
        HighSchool = 2,
        [Display(Name = "Diploma")]
        Diploma = 3,
        [Display(Name = "Associate Degree")]
        AssociateDegree = 4,
        [Display(Name = "Bachelor")]
        Bachelor = 5,
        [Display(Name = "MA")]
        MA = 6,
        [Display(Name = "Doctorate")]
        Doctorate = 7,
        [Display(Name = "Postdoctoral")]
        Postdoctoral = 8
    }
}
