'noDojo
'2.28.2013
'

Imports System.Drawing.Color

Public Class Form1

    Public Rows As Integer
    Public Columns As Integer

    Public Board(,) As Button
    Public ButtSize As Integer = 40
    Public fontsize As Integer = 12
    Public formcenter As New Point(Me.Size.Width / 2, Me.Size.Height / 2)
    Public startXcord As Integer = 1
    Public startYcord As Integer = 1
    Public Count As Integer = 1

    'function that governs the numbering of the buttons in the order that the are clicked

    Private Sub GridButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        CType(sender, Button).ForeColor = Color.Red

        'if there's already something in the box, don't put anything else in

        If (CType(sender, Button).Text = vbNullString) Then
            CType(sender, Button).Text = Count
            Count += 1

        End If

    End Sub

    'function that sets the button size based on the user entered number of columns and rows

    Public Sub setBtnSizes()

        If (Columns > Rows) Then
            ButtSize = Me.Size.Height / Rows
        Else
            ButtSize = Me.Size.Width / Columns
        End If

        If ButtSize > 100 Then 'change font size to accomodate for large buttons
            fontsize = 72
        End If

    End Sub

    'when the button click event takes place, construct a 2d array of controls (buttons)

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Columns = TextBox1.Text
        Rows = TextBox2.Text

        'hide original textboxes and button

        TextBox1.Visible = False
        TextBox2.Visible = False
        Label1.Visible = False
        Label2.Visible = False
        Button2.Visible = False

        setBtnSizes()

        'get the user input board size

        Columns = TextBox1.Text
        Rows = TextBox2.Text
        ReDim Board(Columns, Rows) 'change the dim of board so it can be used dynamically
        Dim color As Integer

        'nested for loop for the creation of the board

        For i As Integer = 0 To Rows - 1

            'determines color to start row with

            If (i Mod 2) Then
                color = 1
            Else
                color = -1
            End If

            For j As Integer = 0 To Columns - 1

                Board(j, i) = New Button
                Board(j, i).Location = New Point(startXcord + j * ButtSize, startYcord + i * ButtSize)
                Board(j, i).Enabled = True
                Board(j, i).Visible = True
                Board(j, i).Font = New Font("Calibri", fontsize)
                Board(j, i).Size = New Size(ButtSize, ButtSize)

                'switches colors back and forth down the row
                'color is int but Im using it as a bool basically

                If (color = 1) Then
                    Board(j, i).BackColor = White
                Else
                    Board(j, i).BackColor = Black
                End If
                color *= -1

                'set handler

                AddHandler Board(j, i).Click, AddressOf GridButtonClick

                Me.Controls.Add(Board(j, i))
                Me.Refresh()
            Next
        Next

    End Sub
End Class
