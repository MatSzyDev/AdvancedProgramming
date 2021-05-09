const uri = "https://localhost:44354/api/BooksItems";

let todos = null;
function getCount(data) {
    const el = $("#counter");
    let name = "to-do";
    if (data) {
        if (data > 1) {
            name = "to-dos";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}
$(document).ready(function () {
    getData();
});
function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#todos");
            $(tBody).empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text("Pozycja " + item.id + ":"))
                    .append($("<td></td>").text(item.name))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );
                tr.appendTo(tBody);
            });
            todos = data;
        }
    });
}
function addItem() {
    const item = {
        name: $("#add-book").val()
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/CreateTodoItem',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-book").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-book").val(item.name);
            $("#edit-id").val(item.id);
        }
    });
    $("#spoiler").css({ display: "block" });
}

function updateItem() {
    var id = parseInt($("#edit-id").val(), 10);
    const item = {
        id: id,
        name: $("#edit-book").val()
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/UpdateTodoItem',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            closeInput();
        }
    });
}

function closeInput() {
    $("#spoiler").css({ display: "none" });
}