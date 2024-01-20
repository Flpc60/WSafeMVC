async function askQuestion() {
    const question = document.getElementById('question').value;
    const responseContainer = document.getElementById('response');

    // Realizar una solicitud a la API de OpenAI
    const response = await getChatGPTResponse(question);

    // Mostrar la respuesta en la interfaz de usuario
    responseContainer.innerHTML = `<p>Respuesta: ${response.choices[0].text}</p>`;
}

async function getChatGPTResponse(question) {
    const apiKey = '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';
    const endpoint = 'https://api.openai.com/v1/engines/davinci-codex/completions';

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${apiKey}`,
    };

    const data = {
        prompt: question,
        max_tokens: 150,
    };

    const response = await fetch(endpoint, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data),
    });

    return await response.json();
}
