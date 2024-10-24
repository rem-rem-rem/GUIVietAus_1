using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateDropdown : MonoBehaviour
{
    public TMP_Dropdown dayDropdown;
    public TMP_Dropdown monthDropdown;
    public TMP_Dropdown yearDropdown;

    public TMP_Text displayDateText; // TextMeshPro để hiển thị ngày đã chọn

    private int selectedYear = 2024; // Default year
    private int selectedDay = 1; // Default day

    private void Start()
    {
        // Populate year dropdown with a range of years
        PopulateYearDropdown();
        // Populate month dropdown with 12 months
        PopulateMonthDropdown();
        // Populate day dropdown based on the selected month and year
        PopulateDayDropdown();

        // Add listeners to update the day dropdown when year or month changes
        yearDropdown.onValueChanged.AddListener(delegate { OnYearChanged(); });
        monthDropdown.onValueChanged.AddListener(delegate { OnMonthChanged(); });

        // Set the default selected day
        dayDropdown.onValueChanged.AddListener(delegate { OnDayChanged(); });

    }

    private void PopulateDayDropdown()
    {
        // Get the selected month from the month dropdown
        int selectedMonth = monthDropdown.value + 1;
        // Calculate the number of days in the selected month and year
        int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);

        // If the selected day exceeds the maximum days in the new month, reset to 1
        if (selectedDay > daysInMonth)
        {
            selectedDay = 1;
        }

        // Clear existing day options
        dayDropdown.ClearOptions();
        // Create a new list of day options
        List<string> dayOptions = new List<string>();
        for (int day = 1; day <= daysInMonth; day++)
        {
            dayOptions.Add(day.ToString("D2"));
        }
        // Add the day options to the day dropdown
        dayDropdown.AddOptions(dayOptions);

        // Set the selected day in the dropdown
        dayDropdown.SetValueWithoutNotify(selectedDay - 1); // SetValueWithoutNotify to avoid triggering unwanted events

        // Cập nhật TextMeshPro khi khởi động
        UpdateDateDisplay();
    }

    // Populate the month dropdown with 12 months
    private void PopulateMonthDropdown()
    {
        // Clear existing month options
        monthDropdown.ClearOptions();
        // Create a new list of month options
        List<string> monthOptions = new List<string>();
        for (int month = 1; month <= 12; month++)
        {
            monthOptions.Add(month.ToString("D2"));
        }

        // Add the month options to the month dropdown
        monthDropdown.AddOptions(monthOptions);
    }

    // Populate the year dropdown with a range of years
    private void PopulateYearDropdown()
    {
        // Clear existing year options
        yearDropdown.ClearOptions();
        // Create a new list of year options
        List<string> yearOptions = new List<string>();
        for (int year = 2023; year <= 2100; year++)
        {
            yearOptions.Add(year.ToString());
        }
        // Add the year options to the year dropdown
        yearDropdown.AddOptions(yearOptions);
    }

    // Update the day dropdown when the year is changed
    private void OnYearChanged()
    {
        // Get the selected year from the year dropdown
        selectedYear = int.Parse(yearDropdown.options[yearDropdown.value].text);
        // Re-populate the day dropdown based on the new year
        PopulateDayDropdown();

        // Cập nhật TextMeshPro khi khởi động
        UpdateDateDisplay();
    }

    // Update the day dropdown when the month is changed
    private void OnMonthChanged()
    {
        // Re-populate the day dropdown based on the new month
        PopulateDayDropdown();

        // Cập nhật TextMeshPro khi khởi động
        UpdateDateDisplay();
    }

    // Store the selected day when the day dropdown is changed
    private void OnDayChanged()
    {
        selectedDay = dayDropdown.value + 1; // Store the currently selected day

        // Cập nhật TextMeshPro khi khởi động
        UpdateDateDisplay();
    }

    // Hàm cập nhật TextMeshPro hiển thị ngày đã chọn
    private void UpdateDateDisplay()
    {
        int selectedMonth = monthDropdown.value + 1;
        displayDateText.text = $"{selectedYear}-{selectedMonth}-{selectedDay}";

        DateTime parsedDate;
        if (DateTime.TryParseExact(displayDateText.text, "yyyy-M-d", null, System.Globalization.DateTimeStyles.None, out parsedDate))
        {
            // Chuỗi đã được chuyển đổi thành DateTime thành công
            Debug.Log("Parsed Date: " + parsedDate.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
        }

        displayDateText.text = parsedDate.ToString("yyyy-MM-ddTHH:mm:ss.fff");
    }
}
