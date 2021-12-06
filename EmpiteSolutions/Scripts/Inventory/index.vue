<template>
    <div id="product">
        <table class="table">
            <thead>
                <tr>
                    <th>
                        Product Description
                    </th>
                    <th>
                        Quantity
                    </th>
                    <th v-show=HasPermission></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in Products">
                    <td>
                        {{item.ItemDescription}}
                    </td>
                    <td>
                        {{item.Quantity}}
                    </td>
                    <td v-show=HasPermission>
                        <a :href="EditUrl+'/'+item.Id">Edit</a>|
                        <a :href="DetailsUrl+'/'+item.Id">Details</a> |
                        <a :href="DeleteUrl+'/'+item.Id">Delete</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
    export default {
        name: "index-component",
        props: {
            CreateProductUrl: String,
            DeleteUrl: String,
            EditUrl: String,
            DetailsUrl: String,
            Products: Array,
            HasPermission: Boolean
        }
    };
</script>