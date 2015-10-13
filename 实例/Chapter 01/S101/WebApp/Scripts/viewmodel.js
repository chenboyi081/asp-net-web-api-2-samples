function ViewModel() {
    self = this;
    self.contacts = ko.observableArray(); //��ǰ��ϵ���б�
    self.contact = ko.observable(); //��ǰ�༭��ϵ��

    //��ȡ��ǰ��ϵ���б�
    self.load = function () {
        $.ajax({
            url: "http://localhost/webhost/api/contacts",
            type: "GET",
            success: function (result) {
                self.contacts(result);
            }
        });
    };

    //�����༭��ϵ�˶Ի���
    self.showDialog = function (data) {
        //ͨ��Id�ж�"���/�޸�"����
        if (!data.Id) {
            data = { ID: "", Name: "", PhoneNo: "", EmailAddress: "", Address: "" }
        }
        self.contact(data);
        $(".modal").modal('show');
    };

    //����Web API���/�޸���ϵ����Ϣ
    self.save = function () {
        $(".modal").modal('hide');
        if (self.contact().Id) {
            $.ajax({
                url: "http://localhost/webhost/api/contacts/" + self.contact.Id,
                type: "PUT",
                data: self.contact(),
                success: function () {
                    self.load();
                }
            });
        }
        else {
            $.ajax({
                url: "http://localhost/webhost/api/contacts",
                type: "POST",
                data: self.contact(),
                success: function () {
                    self.load();
                }
            });
        }
    };

    //ɾ��������ϵ��
    self.delete = function (data) {
        $.ajax({
            url: "http://localhost/webhost/api/contacts/" + data.Id,
            type: "DELETE",
            success: function () {
                self.load();
            }
        });
    };

    self.load();
}

$(function () {
    ko.applyBindings(new ViewModel());
});