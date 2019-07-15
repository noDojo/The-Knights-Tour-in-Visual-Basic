'noDojo
'3.7.2013
''Knight's Tour

Imports System.Drawing.Color

Public Class Form1

    Public Rows As Integer
    Public Columns As Integer

    '2D array of buttons
    Public Board(,) As Button

    Public ButtSize As Integer = 40
    Public fontsize As Integer = 12
    Public formcenter As New Point(Me.Size.Width / 2, Me.Size.Height / 2)
    Public startXcord As Integer = 1
    Public startYcord As Integer = 1
    Public Count As Integer = 0

    Public maxMoves As Integer

    '******************************************************************************************'
    'function that finds the square clicked as the starting point by the user
    'and calls moveKnight to initiate the knight's tour. also calls disable buttons
    'to disallow user input following the initial button click
    '******************************************************************************************'

    Private Sub GridButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        CType(sender, Button).ForeColor = Color.Red 'color of text to red

        'if the button clicked is empty, put a "K" inside of it

        If (CType(sender, Button).Text = vbNullString) Then
            CType(sender, Button).Text = "K"
            Count += 1

            'loop through board and find the square (button) that has been clicked
            'as the starting point

            For i As Integer = 0 To Rows
                For j As Integer = 0 To Columns

                    If (Board(j, i).Text = "K") Then
                        startXcord = j
                        startYcord = i

                    End If

                Next
            Next

            'if moveKnight finds a soltion, return successful completion message and
            'elapsed time from program start to finish

            Dim span As New Stopwatch
            span.Start()
            If (moveKnight(startXcord, startYcord)) Then
                MessageBox.Show("Solution Found!" & _
                                vbCr & "Elapsed Time: " & Format(span.Elapsed.TotalSeconds, "F2") & " seconds")

            Else
                MessageBox.Show("No solution possible.")
            End If

        End If
        disableButtons() 'disallow the buttons from responding to user input

    End Sub

    '******************************************************************************************'
    '               'disallow the buttons from responding to user input
    '******************************************************************************************'

    Private Sub disableButtons()

        For i As Integer = 0 To Rows - 1
            For j As Integer = 0 To Columns - 1

                RemoveHandler Board(j, i).Click, AddressOf GridButtonClick

            Next
        Next

    End Sub

    '******************************************************************************************'
    'function that sets the button size based on the user entered number of columns and rows
    '******************************************************************************************'

    Public Sub setBtnSizes()

        ButtSize = 12

        If (Columns > Rows) Then
            ButtSize = Me.Size.Height / Rows
            fontsize = ButtSize * 0.25
        Else
            ButtSize = Me.Size.Width / Columns
            fontsize = ButtSize * 0.25
        End If

    End Sub

    '******************************************************************************************'
    'when the button click event takes place, construct a 2d array of controls (buttons)
    '******************************************************************************************'

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

        Columns = CInt(TextBox1.Text)
        Rows = CInt(TextBox2.Text)
        ReDim Board(Columns, Rows) 'change the dim of board so it can be used dynamically
        Dim color As Integer

        maxMoves = Columns * Rows

        Columns = Columns - 1
        Rows = Rows - 1

        'nested for loop for the creation of the board

        For i As Integer = 0 To Rows

            'determines color to start row with

            If (i Mod 2) Then
                color = 1
            Else
                color = -1
            End If

            For j As Integer = 0 To Columns

                Board(j, i) = New Button
                Board(j, i).Location = New Point(startXcord + j * ButtSize, startYcord + i * ButtSize)
                Board(j, i).Enabled = True
                Board(j, i).Visible = True
                Board(j, i).Font = New Font("Calibri", fontsize)
                Board(j, i).Size = New Size(ButtSize, ButtSize)

                'switches colors back and forth down the row
                'color is int but Im using it as a bool basically

                Me.ForeColor = Red 'change font color to red for coordinates

                If (color = 1) Then
                    Board(j, i).BackColor = White
                Else
                    Board(j, i).BackColor = Black
                End If
                color *= -1 'switches color back and forth

                'set handler
                ' modify handler to only accept first click (Round 2)

                AddHandler Board(j, i).Click, AddressOf GridButtonClick

                'add board to form and refresh

                Me.Controls.Add(Board(j, i))
                Me.Refresh()

            Next
        Next

        MessageBox.Show("Click a square to begin.")

    End Sub

    '******************************************************************************************'
    'recursive function that:
    '1. checks if the board isFull
    '2. evaluates move options from 8 possible moves
    '3. undoes move(s) if no possible move is available
    '******************************************************************************************'

    Public Function moveKnight(ByVal x As Integer, ByVal y As Integer) As Integer

        Me.Refresh()

        If (isFull(Count) = True) Then 'Full?
            Return 1
        Else 'Not Full

            'conditions for each possible move
            'comments same for each move

            '1********
            If checkPoint(x, y, 1, -2) Then     'call checkPoint to see if the move is possible
                Board(x, y).Text = Count        'if possible, put count in square
                Count += 1                      'increment count
                If (moveKnight(x + 1, y - 2)) Then      'make move and continue on tour
                    Return 1
                End If
            End If
            '2********
            If checkPoint(x, y, 2, -1) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x + 2, y - 1)) Then
                    Return 1
                End If
            End If
            '3********
            If checkPoint(x, y, 2, 1) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x + 2, y + 1)) Then
                    Return 1
                End If
            End If
            '4********
            If checkPoint(x, y, 1, 2) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x + 1, y + 2)) Then
                    Return 1
                End If
            End If
            '5********
            If checkPoint(x, y, -1, 2) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x - 1, y + 2)) Then
                    Return 1
                End If
            End If
            '6********
            If checkPoint(x, y, -2, 1) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x - 2, y + 1)) Then
                    Return 1
                End If
            End If
            '7********
            If checkPoint(x, y, -2, -1) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x - 2, y - 1)) Then
                    Return 1
                End If
            End If
            '8********
            If checkPoint(x, y, -1, -2) Then
                Board(x, y).Text = Count
                Count += 1
                If (moveKnight(x - 1, y - 2)) Then
                    Return 1

                End If
            End If
            End If

        Count -= 1                  'if no moves work, decrement count and
        Board(x, y).Text = ""       'remove the count from the square and
        Return 0                    'return negative

    End Function

    '******************************************************************************************'
    '               check if moves equals maxMoves(columns*rows)
    '******************************************************************************************'

    Public Function isFull(ByVal count As Integer) As Boolean

        If (count = maxMoves) Then
            Return True
        Else
            Return False
        End If

    End Function

    '******************************************************************************************'
    'checks that next move is within the boundaries of the board
    '1. Ex. for first rec call: is (x+1) <= columns?
    '2. Ex. " : is (y -2) >= 0?
    '3. text = currMove
    '4. return positive if move is available and possible
    '******************************************************************************************'

    Public Function checkPoint(ByRef currX As Integer, ByRef currY As Integer, ByRef offsetX As Integer, ByRef offsetY As Integer) As Integer

        Dim newX As Integer = currX + offsetX
        Dim newY As Integer = currY + offsetY

        If (newX <= Columns And newX >= 0) Then
            If (newY <= Rows And newY >= 0) Then
                If Board(newX, newY).Text = "" Then
                    Return 1
                End If
            Else
                Return 0
            End If
        Else
            Return 0
        End If

    End Function

End Class
