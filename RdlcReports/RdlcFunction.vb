Imports Microsoft.VisualBasic

Public Class Class1

    Public Function FormatTimeSpan(timeTicks As Long) As String
        Dim result As String
        result = iif(TimeSpan.FromTicks(timeTicks).Days < 0 Or TimeSpan.FromTicks(timeTicks).Minutes < 0 Or TimeSpan.FromTicks(timeTicks).Hours < 0, "-", "") +
            Math.Abs((TimeSpan.FromTicks(timeTicks).Days * 24 + TimeSpan.FromTicks(timeTicks).Hours)).ToString("d2") +
            ":" +
            Math.Abs(TimeSpan.FromTicks(timeTicks).Minutes).ToString("d2")
        Return result
    End Function
    Public Function LegendColor(isLate As Boolean, isHalfDay As Boolean, isHoliday As Boolean) As String
        If (isLate) Then
            Return "red"
        ElseIf (isHalfDay) Then
            Return "blue"
        ElseIf (isHoliday) Then
            Return "Orange"
        Else
            Return "No Color"
        End If
    End Function

End Class
