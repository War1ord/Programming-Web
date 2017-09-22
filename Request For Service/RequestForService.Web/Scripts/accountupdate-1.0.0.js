$(document).ready(function () {
	// update direct on change
	$('#User_Person_Title').change(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserpersontitle,
			$('#User_Id').val(),
			$('#User_Person_Title').val());
	});
	$('#User_Person_Gender').change(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserpersongender,
			$('#User_Id').val(),
			$('#User_Person_Gender').val());
	});
	$('#User_JobDetails_JobTitle').change(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserjobdetailsjobtitle,
			$('#User_Id').val(),
			$('#User_JobDetails_JobTitle').val());
	});
	$('#User_ReceiveNewsletters').change(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserreceivenewsletters,
			$('#User_Id').val(),
			$('#User_ReceiveNewsletters').isChecked());
	});

	// show update button
	$('#User_Person_FirstName').keypress(function () {
		$("#btnPersonFirstNameUpdate").removeClass("hide");
	});
	$('#User_Person_MiddleName').keypress(function () {
		$("#btnPersonMiddleNameUpdate").removeClass("hide");
	});
	$('#User_Person_LastName').keypress(function () {
		$("#btnPersonLastNameUpdate").removeClass("hide");
	});
	$('#User_ContactDetails_Office').keypress(function () {
		$("#btnContactDetailsOfficeUpdate").removeClass("hide");
	});
	$('#User_ContactDetails_Mobile').keypress(function () {
		$("#btnContactDetailsMobileUpdate").removeClass("hide");
	});

	//button click
	$('#btnPersonFirstNameUpdate').click(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserpersonfirstname,
			$('#User_Id').val(),
			$('#User_Person_FirstName',
			function() {
				$('#btnPersonFirstNameUpdate').fadeOut('hide');
			}).val());
	});
	$('#btnPersonMiddleNameUpdate').click(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserpersonmiddlename,
			$('#User_Id').val(),
			$('#User_Person_MiddleName').val(),
			function () {
				$('#btnPersonMiddleNameUpdate').addClass('hide');
			});
	});
	$('#btnPersonLastNameUpdate').click(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveuserpersonlastname,
			$('#User_Id').val(),
			$('#User_Person_LastName').val(),
			function () {
				$('#btnPersonLastNameUpdate').addClass('hide');
			});
	});
	$('#btnContactDetailsOfficeUpdate').click(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveusercontactdetailsoffice,
			$('#User_Id').val(),
			$('#User_ContactDetails_Office').val(),
			function () {
				$('#btnContactDetailsOfficeUpdate').addClass('hide');
			});
	});
	$('#btnContactDetailsMobileUpdate').click(function () {
		RequestForService.SaveField_PostAction(window.urlActionSaveusercontactdetailsmobile,
			$('#User_Id').val(),
			$('#User_ContactDetails_Mobile').val(),
			function () {
				$('#btnContactDetailsMobileUpdate').addClass('hide');
			});
	});

});

