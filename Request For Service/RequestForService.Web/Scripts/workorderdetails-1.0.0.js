$(document).ready(function () {
	workorder.load();
});

var workorder = {
	load: function() {
		//$("#btn_Item_EntityProperties_Project__Value").click(function () { workorder.click.btnProject($(this)); });
		//$("#btn_Item_EntityProperties_WorkOrderType__Value").click(function () { workorder.click.btnTitle($(this)); });
		//$("#btn_Item_ValueProperties_Priority_").click(function () { workorder.click.btnPriority($(this)); });
		//$("#btn_Item_ValueProperties_WorkOrderReference_").click(function () { workorder.click.btnPriority($(this)); });
		//$("#btn_Item_ValueProperties_Title_").click(function () { workorder.click.btnTitle($(this)); });
		//$("#btn_Item_EntityProperties_HourlyRate__Value").click(function () { workorder.click.btnHourlyRate($(this)); });
		//$("#btn_Item_EntityProperties_AssignedToUser__Value").click(function () { workorder.click.btnAssignedTo($(this)); });
		//$("#btn_Item_ValueProperties_Description_").click(function () { workorder.click.btnDescription($(this)); });

	},
	click: {
		btnProject: function(btn) {
			if (!btn) return;
			RequestForService.Popups.Message("Test", "This is a message popup test.");
		},
		btnType: function(btn) {
			if (!btn) return;

		},
		btnPriority: function(btn) {
			if (!btn) return;

		},
		btnReference: function(btn) {
			if (!btn) return;

		},
		btnTitle: function(btn) {
			if (!btn) return;

		},
		btnHourlyRate: function(btn) {
			if (!btn) return;

		},
		btnAssignedTo: function(btn) {
			if (!btn) return;

		},
		btnDescription: function(btn) {
			if (!btn) return;

		}
	}
}