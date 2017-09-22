var supportEmail = "support@requestforservice.co.za";
var unexpectedErrorMessage = "Unexpected error occurred. Please try again, if the problem persist, please contact us at " + supportEmail;
var requestVerificationToken = "input[name='__RequestVerificationToken']";
var statusbar = '#statusBar';

$(document).ready(function() {

	// on post return to previous scroll position
	if (localStorage['page'] == document.URL.split("?")[0].split("#")[0]) {
		$(document).scrollTop(localStorage['scrollTop']);
		$(document).scrollLeft(localStorage['scrollLeft']);
	} else {
		// if other page reset scroll top and left
		localStorage['scrollTop'] = 0;
		$(document).scrollTop(0);
		localStorage['scrollLeft'] = 0;
		$(document).scrollLeft(0);
	}
/*
	// on form submit set local storage to previous scroll position (use this if you only want to set the scroll position on post) 
	$('form').submit(function () {
		localStorage['page'] = document.URL.split("?")[0].split("#")[0];
		localStorage['scrollTop'] = $(document).scrollTop();
		localStorage['scrollLeft'] = $(document).scrollLeft();
	});
*/

	//TODO: check/test if auto menu hide on lost focus is working...
	$('#SearchText').click(function() {
		$('#navbar').collapse();
	});

	$.fn.isChecked = function() {
		if ($(this).is(":checked")) {
			return true;
		} else {
			return false;
		}
	}

	$.fn.submitForm = function() {
		$(this).closest('form').submit();
	}

	// a nice js extention method to scroll to a tag usage: $('#errorMessage').scrollView();
	// http://stackoverflow.com/questions/1586341/how-can-i-scroll-to-a-specific-location-on-the-page-using-jquery
	$.fn.scrollView = function (offset) {
		return this.each(function () {
			var offsetValue;
			if (offset) {
				offsetValue = offset;
			} else {
				offsetValue = 0;
			}
			$('html, body').animate({
				scrollTop: $(this).offset().top - offsetValue
			}, 400);
		});
	}

});
// on document scroll set local storage to previous scroll position (use this if you want to set the scroll position as you scroll, so meaning if you reload the page the scroll position is saved also)
$(document).scroll(function() {
	localStorage['page'] = document.URL.split("?")[0].split("#")[0];
	localStorage['scrollTop'] = $(document).scrollTop();
	localStorage['scrollLeft'] = $(document).scrollLeft();
});

var RequestForService = {
	QueryString: {
		UpdateQueryString: function(url, key, value) {
			///<summary>a Function to update and return a query string in a url</summary>
			///<returns type="string">The url with the updated Query String</returns>
			if (!url) url = window.location.href;
			var re = new RegExp("([?|&])" + key + "=.*?(&|#|$)(.*)", "gi");

			if (re.test(url)) {
				if (typeof value !== 'undefined' && value !== null)
					return url.replace(re, '$1' + key + "=" + value + '$2$3');
				else {
					return url.replace(re, '$1$3').replace(/(&|\?)$/, '');
				}
			} else {
				if (typeof value !== 'undefined' && value !== null) {
					var separator = url.indexOf('?') !== -1 ? '&' : '?',
					    hash = url.split('#');
					url = hash[0] + separator + key + '=' + value;
					if (hash[1]) url += '#' + hash[1];
					return url;
				} else
					return url;
			}
		},
		GetQueryStringByKey: function getQueryStringByKey(key) {
			key = key.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
			var regex = new RegExp("[\\?&]" + key + "=([^&#]*)"),
			    results = regex.exec(location.search);
			return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
		}
	},
	Containers: {
		Reload: function(url, container, onSuccessCallBack) {
			$.ajax({
				url: url,
				cache: false,
				type: "GET",
				success: function(partialview) {
					$(container).html(partialview);
					if (typeof (onSuccessCallBack) == "function") {
						onSuccessCallBack();
					}
				},
				error: function() {
					alert(unexpectedErrorMessage);
				}
			});
		},
		Append: function(url, container, onSuccessCallBack) {
			$.ajax({
				url: url,
				cache: false,
				type: "GET",
				success: function(partialview) {
					$(container).append(partialview);
					if (typeof (onSuccessCallBack) == "function") {
						onSuccessCallBack();
					}
				},
				error: function() {
					alert(unexpectedErrorMessage);
				}
			});
		}
	},
	Popups: {
		Message: function(header, message) {
			var div = document.createElement("div");
			var name = "popup-dialog";
			div.id = name;
			div.name = name;
			div.innerText = message;
			div.textContent = message;
			div.style.display = 'none';
			document.body.appendChild(div);

			function closePopup() {
				$("#" + div.id).dialog("close");
				$("#" + div.id).remove();
			}

			//hook-up the popup and show
			$("#" + div.id).dialog({
				autoOpen: false,
				width: 800,
				resizable: false,
				modal: true,
				title: "<h2>" + header + "</h2>",
				buttons: {
					"Ok": function () {
						closePopup();
					}
				},
				close: function () {
					closePopup();
				}
			});
			$("#" + div.id).showModelessDialog("open");
		},
		Confirm: function() {
			
		},
		Save: function() {
			
		},
		Show: function (name, header, content, onLoaded, save, saveDataCallBack, onSaveSuccess, onSaveError) {
			var div = document.createElement("div");
			div.id = name;
			div.name = name;
			div.innerText = "Please wait while we process your request.";
			div.textContent = "Please wait while we process your request.";
			div.style.display = 'none';
			document.body.appendChild(div);

			//load html
			RequestForService.Containers.Reload(content, "#" + div.id, onLoaded);

			function closePopup() {
				$("#" + div.id).dialog("close");
				$("#" + div.id).remove();
			}

			//hook-up the popup and show
			$("#" + div.id).dialog({
				autoOpen: false,
				width: 800,
				resizable: false,
				modal: true,
				title: "<h2>" + header + "</h2>",
				buttons: {
					"Save": function () {
						if (typeof (saveDataCallBack) == 'function') {
							RequestForService.Post(save, saveDataCallBack(), onSaveSuccess, onSaveError, closePopup);
						} else {
							RequestForService.Popups.Message("Error", "Unexpected error occurred.");
						}
					},
					"Cancel": function () {
						closePopup();
					}
				},
				close: function () {
					closePopup();
				}
			});
			$("#" + div.id).dialog("open");
		}
	},
	Post: function(url, json, onSuccess, onError) {
		$.ajax({
			url: url,
			cache: false,
			type: "POST",
			data: json,
			success: onSuccess,
			error: onError,
		});
	},
	SaveField_Status: function(url, id, value, statusSelector) {
		$(statusSelector).html('<div class="message-Information">Saving...</div>');
		RequestForService.Post(url, {
				__RequestVerificationToken: $(requestVerificationToken).val(),
				id: id,
				value: value,
			},
			function(data) {
				if (data.success) {
					$(statusSelector).html(
						"<div class=\"message-Information\">" +
						data.message +
						"</div>");
				} else {
					$(statusSelector).html(
						"<div class=\"message-Error\">" +
						data.message +
						"</div>");
				}
			},
			function(request, status, error) {
				$(statusSelector).html('<div class="message-Error">Unexpected error occurred.</div>');
				alert("Unexpected error occurred." + "<br/><br/>Message: " + error);
			}
		);
	},
	SaveField_PostAction: function(url, id, value, postSuccessAction) {
		$(statusbar).hide();
		$(statusbar).html("Saving...");
		$(statusbar).removeClass('field-validation-error');
		$(statusbar).addClass('alert-info');
		$(statusbar).removeClass('hide');
		$(statusbar).fadeIn();
		RequestForService.Post(url, {
			__RequestVerificationToken: $(requestVerificationToken).val(),
			id: id,
			value: value,
		}, function(result) {
			$(statusbar).removeClass('alert-info');
			$(statusbar).hide();
			$(statusbar).html(result.Message);
			if (result.IsSuccessful) {
				$(statusbar).removeClass('field-validation-error');
				$(statusbar).addClass('alert-success');
			} else {
				$(statusbar).removeClass('alert-success');
				$(statusbar).addClass('field-validation-error');
			}
			$(statusbar).removeClass('hide');
			$(statusbar).fadeIn();
			if (postSuccessAction != null && typeof (postSuccessAction) == "function") {
				postSuccessAction();
			}
		}, function(request, status, error) {
			$(statusbar).removeClass('alert-info');
			$(statusbar).html(error);
			$(statusbar).removeClass('alert-success');
			$(statusbar).addClass('field-validation-error');
			$(statusbar).fadeIn();
		});
	}
}