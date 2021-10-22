<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ToDoList.aspx.cs" Inherits="ToDoList.View.ToDoList" %>

<asp:Content ID="ContentToDoList" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css">
    <link href="../Content/Site.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>

    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.js"></script>
    <script src="../Scripts/ToDoList.js"></script>
    <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
    
    <div class="jumbotron text-center">
        <h2>To Do List Test</h2>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-md-4 left-panel">
                <div class="row">
                    <div class="col-md-6">
                        <input id="category" type="text" class="form-control" placeholder="Caetgory" />
                    </div>
                    <div class="col-md-4">
                        <input id="btnAddCategory" type="button" value="Add Category" class="btn btn-primary" />
                    </div>
                    <div class="col-md-2">
                        <button id="btnDelete" type="button" value="" class="btn btn-danger">
                            <span class="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="row">
                    <div class="col-md-4">
                        <input id="itemCategory" type="text" class="form-control" placeholder="Caetgory" />
                    </div>
                    <div class="col-md-4">
                        <input id="item" type="text" class="form-control" placeholder="Item" />
                    </div>
                    <div class="col-md-2">
                        <input id="btnAddItem" type="button" value="Add Item" class="btn btn-primary" />
                    </div>
                    <div class="col-md-2">
                        <button id="btnItemDelete" type="button" value="" class="btn btn-danger">
                            <span class="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
                </div>
            </div>
        </div>
         <button type="button" class="btn btn-default btn-xs pull-right view-item" value="test" onclick="myFunction(this)">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>

        <div class="row to-do-liner"></div>
        <div class="row">
            <div class="col-md-12 text-center">
                <div id="Result"></div>
            </div>
        </div>
        <%--List of Category and Items--%>
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4 left-panel">
                            <div class="row text-center ">
                                <div class="col-md-8">
                                    <h3 class="form-control list-header">Categories</h3>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8" id="category_list" >
                                    <%--<ul class="list-group cat-group">
                                        <li class="list-group-item">
                                            <span class="name">Cooking</span>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-th-list"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right edit-edit">
                                                <span class="glyphicon glyphicon-edit"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right view-item" value="test">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>
                                    </ul>
                                    <ul class="list-group cat-group">
                                        <li class="list-group-item" id="category-list">
                                            <span class="name">Repari</span>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-th-list"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right remove-edit">
                                                <span class="glyphicon glyphicon-edit"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>
                                    </ul>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="row text-center">
                                <div class="col-md-12">
                                    <h3 class="form-control list-header">To do list items</h3>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <ul class="list-group" id="item-list">
                                        <%--<li class="list-group-item">
                                            <span class="name">Cake</span>
                                            <button class="btn btn-default btn-xs pull-right remove-edit">
                                                <span class="glyphicon glyphicon-edit"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>
                                        <li class="list-group-item">
                                            <span class="name">Spaghetti</span>
                                            <button class="btn btn-default btn-xs pull-right remove-edit">
                                                <span class="glyphicon glyphicon-edit"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>
                                        <li class="list-group-item">
                                            <span class="name">Soup</span>
                                            <button class="btn btn-default btn-xs pull-right remove-edit">
                                                <span class="glyphicon glyphicon-edit"></span>
                                            </button>
                                            <button class="btn btn-default btn-xs pull-right remove-item">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>--%>
                                    </ul>                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>    
    <script type="text/javascript">
        
        
    </script>
</asp:Content>
