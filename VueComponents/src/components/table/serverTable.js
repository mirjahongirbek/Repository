export default {
    props: {
        url: {
            type: String,
            required: true
        },
        start: {
            type: Boolean,
            default: true,
        },
        serviceName: {
            default: string,
            required: true

        }
    },
    data() {
        return {
            columns: [],
            options: {
                requestAdapter(data) {
                    return {
                        offset: (data.page - 1) * data.limit,
                        limit:data.limit
                    }
                },
                requestFunction: function (data) {
                    console.log(data);
                    console.log(this);
                }
            }
        }
    },
    methods: {
        getColumns(_this) {
            return new Promise((resolve, reject) => {
                _this.$store.state.http.get(_this.url + "\GetProps?id" + _this.serviceName).then(response => {
                    if (response.data && response.data.result) {
                        _this.columns = response.data.result;
                        resolve();
                    }
                    reject();
                }, err => {
                    _this.$store.getters.errorParse(err, _this);
                    reject();
                })
            });
        },
        getData(start) {
            let _this = this;
            if (!_this.url) return;
            this.getColumns(_this).then(result => {
                _this.$store.state.http.get().then(response => {
                }, err => {
                    _this.$store.getters.errorParse(err, _this);
                })
            }, err => { });


        }
    },
    mounted() {

    },
    watch: {
        "start": function (val) {

        }
    },

}