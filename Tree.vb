Friend Class Node
    Public Value As String
    Public nextNode As Node
End Class

Public Class Tree
    Private _root As Node
    Private _result As String

    Private Sub New()
    End Sub

    Sub New(ByVal rootValue As String)
        _root = AddNode(rootValue)
    End Sub

    Public Sub AddtoTree(ByVal value As String)

        Dim currentNode As Node = _root
        Dim nextNode As Node = _root

        Do While currentNode.Value <> value And nextNode IsNot Nothing
            currentNode = nextNode
            nextNode = nextNode.nextNode
        Loop
        If currentNode.Value <> value Then
            currentNode.nextNode = AddNode(value)
        End If
    End Sub

    Private Sub inOrderTraverse(ByVal node As Node)
        _result = ""
        If Not node Is Nothing Then
            _result &= node.Value & "\"
            inOrderTraverse(node.nextNode)
        End If
    End Sub

    Private Function AddNode(ByVal value As String) As Node
        Dim node As New Node()
        node.Value = value
        Return node
    End Function

    Public Sub Clear()


    End Sub

    Public Function TraverseTree() As String
        inOrderTraverse(_root)
        Return _result
    End Function
End Class
