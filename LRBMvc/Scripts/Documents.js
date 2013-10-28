/*
ko.bindingHandlers.fileUpload = {
	init: function (element, valueAccessor) {
		var $element = $(element);
		var fileNameVal = valueAccessor;
		var uploader = new qq.FineUploader({
			element: $element[0],
			request: {
				endpoint: 'server/your_upload_service'
			},
			multiple: true,
			validation: {
				allowedExtensions: ['jpeg', 'jpg', 'txt']
			},
			callbacks: {
				onComplete: function (id, fileName, responseJSON) {
					if (responseJSON.success) {
						// update your value
						valueAccessor(fileName)
						//may need to change this if you pass a reference back 
						// in your responseJSON such as an s3 key
					}
				}
			}
		});
	}
};
*/
var viewmodel = function () {
	var documents = ko.observableArray(),
		docToUpload = ko.observable(),
		desc = ko.observable();

	var upload = function () {
	    $.post("/api/Documents", {
	        Description: desc(),
	    }, function (data, err) {
	        console.log("err" + err);
	        console.log("success" + data);
	    }, "json");
	};

	var init = function () {
		$.getJSON("/api/Documents/", function (data) {
			dx = JSON.parse(data);

			for (var i = 0; i < dx.length; i++) {
			    documents.push(dx[i]);
			}
		});
	};

	return {
		documents: documents,
		description: desc,
		file: docToUpload,
		upload: upload,
		init: init
	}
}
var vm = viewmodel()
ko.applyBindings(vm);
vm.init();


