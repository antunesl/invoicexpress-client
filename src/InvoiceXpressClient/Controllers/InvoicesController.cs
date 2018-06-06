﻿using InvoiceXpress.Model;
using InvoiceXpressApiClient.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvoiceXpressApiClient.Controllers
{
    public class InvoicesController : BaseController
    {
        public InvoicesController(HttpClient httpClient, string apiKey, ILogger logger) : base(httpClient, apiKey, logger)
        {

        }


        public async Task<InvoiceCollection> GetAll(string page, string pageSize)
        {
            return await GetAsync<InvoiceCollection>("/invoices.json");
        }



        public async Task<Invoice> CreateInvoice(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/invoices.json", new { invoice = newInvoice });
        }

        public async Task<Invoice> CreateSimplifiedInvoice(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/simplified_invoices.json", newInvoice);
        }

        public async Task<Invoice> CreateInvoiceReceipt(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/invoice_receipts.json", newInvoice);
        }

        public async Task<Invoice> CreateVatMossInvoice(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/vat_moss_invoices.json", newInvoice);
        }

        public async Task<Invoice> CreateCreditNote(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/credit_notes.json", newInvoice);
        }

        public async Task<Invoice> CreateDebitNote(Invoice newInvoice)
        {
            return await CreateInvoiceInternal("/debit_notes.json", newInvoice);
        }




        //Creating new clients or items along with the invoice
        //This method also allows to create a new client and/or new items in the same request with the following behavior:
        //If the client name does not exist a new one is created.
        //If items do not exist with the given names, new ones will be created.
        //If item name already exists, the item is updated with the new values.
        //Taxes
        //Regarding item taxes, if the tax name is not found, the default tax is applyed to that item.Portuguese accounts should also send the IVA exemption reason if the invoice contains exempt items (IVA 0%).
        //Note: Simplified Invoices are only available in Portugal.
        private async Task<Invoice> CreateInvoiceInternal(string url, object request)
        {
            return await PostAsync<Invoice>(url, request);
        }
    }


    public class InvoiceCollection : PagedResponse
    {
        [JsonProperty("invoices")]
        public List<Invoice> Invoices { get; set; }
    }


    public class PagedResponse
    {
        [JsonProperty("pagination")]
        public PaginationInfo Pagination { get; set; }
    }
}
