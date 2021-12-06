<template>
    <div id="product">
        <form :action="SendMailUrl" method="post">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-danger"></div>
                    <div class="form-group">
                        <label for="ToEmail" class="control-label">To Email</label>
                        <input name="ToEmail" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="Subject" class="control-label">Subject</label>
                        <input name="Subject" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="EmailBody" class="control-label">Content</label>
                        <input name="EmailBody" class="form-control" />
                    </div>
                    <div class="form-group">
                        <input type="submit" value="Send" class="btn btn-primary" />
                    </div>
                </div>
            </div>
        </form>
    </div>
</template>
<script>
    export default {
        name: "send-mail-component",
        props: {
            SendMailUrl: String
        }
    };
</script>