; (function($) {
    "use strict";
    
    // dependencies
    // jquery.js / zepto.js
    // jquery.form.js

    $.fn.uploads = function (options) {

        $.each(this, function() {

            var $form = $(this);

            if ($form.data("uploadControl")) return;

            if (!this.tagName || this.tagName.toLowerCase() != "form")
                throw "uploads() method can only be called on a form element";

            $form.data(new control($form, options));
        });
    };

    var control = function ($form, options) {

        options = $.extend({}, $.upload.defaults, options);

        $form.submit(function () {
            window.console.log("submit");
            
            if (statusTimeout) return false;

            var statusId = Math.floor(Math.random() * 9999999999999999);

            $(this).ajaxSubmit({
                url: options.url,
                method: options.method,
                beforeSend: function(xhr) {
                    window.console.log("beforeSend");
                    
                    xhr.setRequestHeader("x-upload-status-id", statusId);
                    options.start();
                },
                success: function (data) {
                    window.console.log(data);
                    
                    complete();
                    options.complete(data);
                    options.success(data);
                },
                error: function(data) {
                    window.console.log(data);

                    complete();
                    options.complete(data);
                    options.error(data);
                }
            });

            getStatus(statusId);
            return false;
        });

        var statusTimeout,
            getStatus = function(statusId, delay) {

                statusTimeout = window.setTimeout(
                    function() {
                        $.get(options.urlStatus + statusId, function(status) {

                            if (status == null) {
                                if (delay < 50000) delay *= 2;
                            } else {
                                options.status(status);
                                delay = null;
                            }
                            getStatus(statusId, delay);
                        });
                    }, delay || 1000);
            },
            complete = function() {
                window.console.log("complete " + statusTimeout);
                if (statusTimeout) {
                    window.clearTimeout(statusTimeout);
                    statusTimeout = null;
                }
            };

        window.console = window.console || {
            log: function() {
            }
        };
    };

    $.upload = {
        defaults: {
            url: "/api/upload/",
            urlStatus: "/api/upload/",
            method:"POST",
            start: function () {
            },
            status: function(status) {
            },
            complete: function (response) {
            },
            success: function(response) {
            },
            error: function(errorResponse) {
            },
        }
    };

    })((typeof (jQuery) != 'undefined') ? jQuery : window.Zepto);