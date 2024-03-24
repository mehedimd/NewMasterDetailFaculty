// for addBtn Student ADD
$("#addBtn").click(() => {
    createRowForStudent();
})
function createRowForStudent() {
    let selectedData = getStudentData();
    let index = $("#studentTable").children("tr").length;
    let tr = `<tr>
                                    <td><input type="number" class="form-control" name="Students[${index}].ID" value="${selectedData.sId}" required placeholder="Student ID.."/></td>
                                    <td><input type="text" class="form-control" name="Students[${index}].StudentName" value="${selectedData.sName}" required placeholder="Student Name.."/></td>
                                    <td><input type="text" class="form-control" name="Students[${index}].Address" value="${selectedData.sAddress}" required placeholder="Student Address.."/></td>
                                    <td><input class="btn btn-danger" type="button" value="Remove" id="deleteBtn" /></td>
                                 </tr>`
    $("#studentTable").append(tr);
}
function getStudentData() {
    let studentObj = {
        sId: $("#sId").val(),
        sName: $("#sName").val(),
        sAddress: $("#sAddress").val(),
    }
    return studentObj;
}

// for Remove() Student
function Remove(e) {
    e.closest("tr").remove();
}