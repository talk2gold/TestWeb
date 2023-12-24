$(document).ready(function () {
	//alert("hello");
	loadDatatable();
});

//flip
//f-filter   search
//l-lenght   10,20,30
//i-information  10 of 109 records
//p-pagination 1,2,3,4
function loadDatatable() {
	$('#empdataTable').DataTable(
		{
			fixedColumns: true,
			paging: false,
			scrollCollapse: true,
			scrollX: true,
			scrollY: 300,
			select: true,
			dom: `lftpi`,
			"ajax": { url: '/Employee/GetEmployeeData' },
			rowId: 'empNo',
			"columns": [
				{ "data": 'firstName'  },
				{ "data": 'lastName'  },
				{ "data": 'salary'  },
				{ "data": 'joinDate'  },
				{ "data": 'deptName'  },
				{ "data": 'mgrName'  },
				{ "data": 'mgrName'  }
			]
		});
}

