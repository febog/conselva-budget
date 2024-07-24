﻿// <auto-generated />
using System;
using ConselvaBudget.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConselvaBudget.Data.ConselvaMigrations
{
    [DbContext(typeof(ConselvaBudgetContext))]
    [Migration("20240724223455_AddPaymentMethod")]
    partial class AddPaymentMethod
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConselvaBudget.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ConselvaBudget.Models.AccountAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AccountAssignments");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("ResultId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ActivityBudget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountAssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Comments")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EquivalentAccount")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AccountAssignmentId");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityBudgets");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ActivityBudgetLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityBudgetId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("EventAuthorUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityBudgetId");

                    b.ToTable("ActivityBudgetLogEntries");
                });

            modelBuilder.Entity("ConselvaBudget.Models.AmountRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityBudgetId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("ModifiedByUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityBudgetId");

                    b.HasIndex("RequestId");

                    b.ToTable("AmountRequests");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Comments")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Donor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("ConselvaBudget.Models.EquivalentAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DonorId");

                    b.ToTable("EquivalentAccounts");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ExpenseInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityBudgetId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<decimal>("InvoiceAmount")
                        .HasColumnType("money");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModifiedByUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("PdfUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("XmlUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityBudgetId");

                    b.HasIndex("RequestId");

                    b.ToTable("ExpenseInvoices");
                });

            modelBuilder.Entity("ConselvaBudget.Models.NotificationRecipient", b =>
                {
                    b.Property<string>("AspNetUserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NotificationEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("AspNetUserId");

                    b.ToTable("NotificationRecipients");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Segment")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestorUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("ConselvaBudget.Models.RequestLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EventAuthorUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestLogEntries");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Trip", b =>
                {
                    b.Property<int>("SpendingRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Destination")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Driver")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Participants")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SelectedDates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("SpendingRequestId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("ConselvaBudget.Models.AccountAssignment", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Account", "Account")
                        .WithMany("AccountAssignments")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Organization", "Organization")
                        .WithMany("AccountAssignments")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Activity", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Result", "Result")
                        .WithMany("Activities")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Result");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ActivityBudget", b =>
                {
                    b.HasOne("ConselvaBudget.Models.AccountAssignment", "AccountAssignment")
                        .WithMany("ActivityBudgets")
                        .HasForeignKey("AccountAssignmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Activity", "Activity")
                        .WithMany("ActivityBudgets")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AccountAssignment");

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ActivityBudgetLogEntry", b =>
                {
                    b.HasOne("ConselvaBudget.Models.ActivityBudget", "ActivityBudget")
                        .WithMany()
                        .HasForeignKey("ActivityBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityBudget");
                });

            modelBuilder.Entity("ConselvaBudget.Models.AmountRequest", b =>
                {
                    b.HasOne("ConselvaBudget.Models.ActivityBudget", "ActivityBudget")
                        .WithMany("AmountRequests")
                        .HasForeignKey("ActivityBudgetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Request", "Request")
                        .WithMany("AmountRequests")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ActivityBudget");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Deposit", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Project", "Project")
                        .WithMany("Deposits")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ConselvaBudget.Models.EquivalentAccount", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Account", "Account")
                        .WithMany("EquivalentAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Donor", "Donor")
                        .WithMany("EquivalentAccounts")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ExpenseInvoice", b =>
                {
                    b.HasOne("ConselvaBudget.Models.ActivityBudget", "ActivityBudget")
                        .WithMany("ExpenseInvoices")
                        .HasForeignKey("ActivityBudgetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Request", "Request")
                        .WithMany("ExpenseInvoices")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ActivityBudget");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Project", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Donor", "Donor")
                        .WithMany("Projects")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Request", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Activity", "Activity")
                        .WithMany("SpendingRequests")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("ConselvaBudget.Models.RequestLogEntry", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Request", "Request")
                        .WithMany("RequestLogEntries")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Result", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Project", "Project")
                        .WithMany("Results")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Trip", b =>
                {
                    b.HasOne("ConselvaBudget.Models.Request", "SpendingRequest")
                        .WithOne("Trip")
                        .HasForeignKey("ConselvaBudget.Models.Trip", "SpendingRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConselvaBudget.Models.Vehicle", "Vehicle")
                        .WithMany("Trips")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SpendingRequest");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Account", b =>
                {
                    b.Navigation("AccountAssignments");

                    b.Navigation("EquivalentAccounts");
                });

            modelBuilder.Entity("ConselvaBudget.Models.AccountAssignment", b =>
                {
                    b.Navigation("ActivityBudgets");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Activity", b =>
                {
                    b.Navigation("ActivityBudgets");

                    b.Navigation("SpendingRequests");
                });

            modelBuilder.Entity("ConselvaBudget.Models.ActivityBudget", b =>
                {
                    b.Navigation("AmountRequests");

                    b.Navigation("ExpenseInvoices");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Donor", b =>
                {
                    b.Navigation("EquivalentAccounts");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Organization", b =>
                {
                    b.Navigation("AccountAssignments");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Project", b =>
                {
                    b.Navigation("Deposits");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Request", b =>
                {
                    b.Navigation("AmountRequests");

                    b.Navigation("ExpenseInvoices");

                    b.Navigation("RequestLogEntries");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Result", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("ConselvaBudget.Models.Vehicle", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
