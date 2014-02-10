var hnblog = function () {
    var dateFormat = "h:mma MMM D yyyy";    // 9:34am Oct 5th 2014
    // format dateTime 
    var formatDate = function (input) {
        var timestamp = new Date(input);
        // using jquery format date function
        return $.format.date(timestamp, dateFormat);
    };
    return {
        formatDate: formatDate,
        // remove an entry 
        removeEntry: function (elm) {
            var removeId = $(elm).data("info").id;
            if (removeId != null)
                $.ajax({
                    url: "./Blog/DeletePost",
                    type: "POST",
                    data: $.toJSON({ postID: removeId }),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (data != null) {
                            $(".article-" + removeId).remove();
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
        },
        // add an entry to database
        addEntry: function () {
            var title = $("#title");
            if (title.val() == "") {
                return false;
            }
            var content = $("#content");
            $.ajax({ type: "POST",
                url: "./Blog/CreatePost",
                contentType: "application/json; charset=utf-8",
                data: $.toJSON({ title: title.val(), content: content.val() }),
                dataType: "json",
                success: function (data) {
                    // insert newest entry to the top of the posts
                    var $entryContainer = $("#entries");
                    var article = generateAnArticle(data.Id, data.Title, data.PostContent, new Date(parseInt(data.CreatedDate.substr(6))));
                    $(article).prependTo($entryContainer);
                    // clear input boxes
                    title.val("");
                    content.val("");
                    // scroll the window to the top
                    window.scrollTo(0, 0);
                    $entryContainer.offset().top;
                },
                error: function (xhr, errorStatus, errorCode) {
                    alert(errorCode);
                }
            });

        }
    }// end of public functions

    // private function to generate a post block
    function generateAnArticle(id, title, content, createdDate) {
        return "<article class=\"article-" + id.toString() + "\">\
                            <header>\
                            <h2 class=\"entry-title\">\
                            <a href=\"#\" class=\"link\" data-info='{\"id\":\"" + id + "\"}'>" + title + "</a>\
                            </h2>\
                            </header> <!--end entry-title-->\
                            <div class=\"entry-content\"> <p>" + content + "</p>\
                             </div> <!--end entry-content-->\
                            <footer>\
                                <div class=\"entry-meta\">\
                                <span><a href=\"#\"  data-info='{\"id\":\"" + id + "\"}'  onclick=\"removeEntry(this)\">Remove</a></span>\
                                <span class=\"time\" data-date=\"" + createdDate + "\">" + formatDate(createdDate) + "</span>\
                                </div>\
                            </footer> <!--end footer-->\
                        </article><!--end article-->\
                    </div> <!--end main-->";
    }
} ();
$(document).ready(function () {
    // attache addEntry event to save button
    $("#save").on("click", hnblog.addEntry);
    // format datetime
    $(".entry-meta span[data-date]").each(function (i, item) {
        var $span = $(item);
        $span.html(hnblog.formatDate($span.data("date")));
    });

});

