using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class AuditContext : DbContext
{
    public DbSet<Audit> Audits { get; set; }
    public DbSet <Section> Sections { get; set; }
    public DbSet <Question> Questions { get; set; }
    public DbSet <Response> Responses { get; set; }

    public string DbPath { get; }

    public AuditContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "audit.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source={DbPath}");
    }
}

public class Audit
{
    public int AuditId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<Section> Sections { get; set; }
}

public class Section
{
    public int SectionId { get; set; }
    public string Name { get; set; }
    public int AuditId { get; set; }
    public Audit Audit { get; set; }
    public List<Question> Questions { get; } = new();
}

public class Question
{
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public int SectionId { get; set; }
    public Section Section { get; set; }
    public List<Response> Responses { get; } = new();
}

public enum RadioResponse
{
    Excellent,
    Good, 
    RequiresImprovement, 
    Poor,
    NotAssessed
}

public class Response
{
    public int ResponseId { get; set; }
    public RadioResponse RadioAnswer { get; set; }
    public string TextAnswer { get; set; }
    public string AttachmentPath { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}